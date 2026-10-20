namespace ssilab1;
public class Stan{
    public int[] U = new int[0];
    public List<List<double>>center=new List<List<double>>();
}
public static class KSrednie{
    public static List<List<double>>LosujSrodki(List<List<double>> punkty, int k, Random rnd){
        if(k>punkty.Count){
            throw new ArgumentException("k jest większe od liczby punktów");
        }
        List<int>indeksy=new List<int>();
        while(indeksy.Count<k){
            int i=rnd.Next(punkty.Count);
            if(!indeksy.Contains(i)){
                indeksy.Add(i);
            }
        }
        List<List<double>>srodki=new List<List<double>>();
        foreach(int i in indeksy){
            srodki.Add(new List<double>(punkty[i]));
        }
        return srodki;
    }
    public static int[] Przypisz(List<List<double>>punkty,List<List<double>>srodki,Func<List<double>,List<double>,double> odleglosc){
        int[] u=new int [punkty.Count];
        for(int s=0;s<punkty.Count;s++){
            int najblizszy=0;
            double minodl=odleglosc(punkty[s],srodki[0]);
            for(int j=1;j<srodki.Count;j++){
                double d=odleglosc(punkty[s],srodki[j]);
                if(d<minodl){
                    minodl=d;
                    najblizszy=j;
                }
            }
            u[s]=najblizszy;
        }
        return u;
    }
    public static void PrzeliczSrodki(List<List<double>> punkty, List<List<double>> srodki, int[] u){
        int n=punkty[0].Count;
        for(int j=0;j<srodki.Count;j++){
            double[] suma=new double[n];
            int ile=0;
            for(int s=0;s<punkty.Count;s++){
                if(u[s]==j){
                    for(int i=0;i<n;i++){
                        suma[i]+=punkty[s][i];
                    }
                    ile++;
                }
            }
            if(ile==0){
                continue;
            }
            for(int i=0;i<n;i++){
                srodki[j][i]=suma[i]/ile;
            }
        }
    }
    public static int[] Uruchom(List<List<double>> punkty, List<List<double>> srodki, int iters,Func<List<double>,List<double>,double> odleglosc, int zapispo,out Stan stanzapis, out int iteracje, out bool zbiezny){
        int[] u= new int[punkty.Count];
        stanzapis=new Stan();
        int[] poprzednie=new int[punkty.Count];
        for(int s=0;s<poprzednie.Length;s++){
            poprzednie[s]=-1;
        }
        int ostatniazmiana=0;
        for(int it=0;it<iters;it++){
            u=Przypisz(punkty,srodki,odleglosc);
            bool bylazmiana=false;
            for(int s=0;s<u.Length;s++){
                if(u[s]!=poprzednie[s]){
                    bylazmiana=true;
                }
            }
            if(bylazmiana==true){
                ostatniazmiana=it+1;
            }
            PrzeliczSrodki(punkty,srodki,u);
            if(it+1==zapispo){
                stanzapis=Kopiuj(u,srodki);
            }
            poprzednie=u;
        }
        iteracje=ostatniazmiana;
        if(ostatniazmiana<iters){
            zbiezny=true;
        }else{
            zbiezny=false;
        }
        return u;
    }
    public static Stan Kopiuj(int[] u, List<List<double>> center){
        Stan st=new Stan();
        st.U=new int[u.Length];
        for(int s=0;s<u.Length;s++){
            st.U[s]=u[s];
        }
        st.center=new List<List<double>>();
        for(int j=0;j<center.Count;j++){
            st.center.Add(new List<double>(center[j]));
        }
        return st;
    }
    public static string Raport(List<List<double>> punkty, List<List<double>> srodki, int[] u){
        string tekst="";
        int n=punkty[0].Count;
        for(int j=0;j<srodki.Count;j++){
            double[] min=new double[n];
            double[] max =new double[n];
            for(int i=0;i<n;i++){
                min[i]=double.MaxValue;
                max[i]=double.MinValue;
            }
            int ile=0;
            string numery="";
            for(int s=0;s<punkty.Count;s++){
                if(u[s]==j){
                    ile++;
                    numery+=(s+1)+" ";
                    for(int i=0;i<n;i++){
                        if(punkty[s][i]<min[i]){
                            min[i]=punkty[s][i];
                        }
                        if(punkty[s][i]>max[i]){
                            max[i]=punkty[s][i];
                        }
                    }
                }
            }
            tekst+="Grupa "+(j+1)+", probek: "+ile+Environment.NewLine;
            tekst+=" środek:";
            for(int i=0;i<n;i++){
                tekst+=" x"+(i+1)+"="+srodki[j][i].ToString("F4");
            }
            tekst+=Environment.NewLine;
            if(ile==0){
                tekst+=" (grupa pusta)"+Environment.NewLine;
            }else{
                for(int i=0;i<n;i++){
                    tekst+=" x"+(j+1)+" min="+min[i].ToString("F4")+" max="+max[i].ToString("F4")+Environment.NewLine;
                }
                tekst+=" probki: "+numery+Environment.NewLine;
            }
        }
        return tekst;
    }
}