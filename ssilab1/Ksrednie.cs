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
}