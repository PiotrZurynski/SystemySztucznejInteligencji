using System.Globalization;
using System.Reflection;
namespace ssilab1;

public partial class Form1 : Form
{
    string sciezka="";
    Random rnd = new Random();
    NumericUpDown numK;
    NumericUpDown numIter;
    ComboBox comboMetody;
    TextBox txtRaport;
    Label lblWynik;
    string dane4="";
    string daneKoniec="";

    public Form1()
    {   
        InitializeComponent();
        Text="Metody";
        Size=new Size(500,666);

        var lblDane = new Label{Text="Dane",Left=30,Top=30,AutoSize=true};
        var btnSzukaj= new Button{Text="Szukaj", Left=75,Top=30,Width=100,Height=30};
        btnSzukaj.Click+=BrowseClick;
        var lblnumK = new Label{Text="k", Left=30,Top=70,Width=20};
        numK=new NumericUpDown{Left=50,Top=70,Width=50,Minimum=2,Maximum=100,Value=4};
        var lblIter = new Label{Text="Liczba iteracji:",Left=30,Top=100,AutoSize=true};
        numIter=new NumericUpDown{Left=140,Top=100,Value=10,Maximum=100,Minimum=1};
        var lblMetoda = new Label{Text="Metoda:",Left=30,Top=140,AutoSize=true};
        comboMetody=new ComboBox{Left=110,Top=135,Width=200,DropDownStyle=ComboBoxStyle.DropDownList};
        txtRaport=new TextBox{Left=30,Top=370,Width=430,Height=220,Multiline=true,ScrollBars=ScrollBars.Vertical};
        var btnStart=new Button{Text="Start",Left=30,Top=170,Width=90,Height=30};
        btnStart.Click+=BtnOblicz_Click;
        var btnKopiuj4=new Button{Text="Kopiuj (4 iter.)",Left=130, Top=170,Width=110,Height=30};
        btnKopiuj4.Click+=Kopiuj4_Click;
        var btnKopiujKoniec=new Button{Text="Kopiuj koniec", Left=250,Top=170,Width=110,Height=30};
        btnKopiujKoniec.Click+=KopiujKoniec_Click;
        lblWynik=new Label{Left=30,Top=205,Width=300,Height=120,Text=""};

        Controls.Add(lblDane);
        Controls.Add(btnSzukaj);
        Controls.Add(lblnumK);
        Controls.Add(numK);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(btnStart);
        Controls.Add(lblWynik);
        Controls.Add(lblMetoda);
        Controls.Add(comboMetody);
        Controls.Add(txtRaport);
        Controls.Add(btnKopiuj4);
        Controls.Add(btnKopiujKoniec);
        ZaladujMetody();

    }
    void BtnOblicz_Click(object? sender, EventArgs e){
        try{
            List<List<double>>punkty = WczytajCsv(sciezka);
            int k=(int)numK.Value;
            int iters=(int)numIter.Value;
            var srodki=KSrednie.LosujSrodki(punkty,k,rnd);
            var metoda=typeof(Metody).GetMethod(comboMetody.Text);
            //lblWynik.Text="Środek 1: x1="+srodki[0][0] +" x2="+srodki[0][1];
            //lblWynik.Text="Wczytano punktów: "+punkty.Count+", wymiar: "+punkty[0].Count+"\nk: "+k+", iteracji: "+iters;
            Func<List<double>,List<double>,double> odleglosc=(Func<List<double>,List<double>,double>)Delegate.CreateDelegate(typeof(Func<List<double>,List<double>,double>),metoda);
            Stan stan4;
            int wykonane;
            bool zbiezny;
            int[] u=KSrednie.Uruchom(punkty,srodki,iters,odleglosc,4,out stan4,out wykonane,out zbiezny);
            int[] ile=new int[k];
            foreach(int g in u){
                ile[g]++;
            }
            string opis="";
            if(zbiezny==true){
                opis=" (zbiezne, po "+wykonane+" iteracjach)";
            }else{
                opis=" (brak zbieznosci w "+iters+" iteracjach)";
            }
            lblWynik.Text="Licznosc: "+string.Join(", ",ile)+"\nIteracji: "+iters+opis;
            string raport="";
            if(iters>4){
                raport+="Stan po 4 iteracjach"+Environment.NewLine+KSrednie.Raport(punkty,stan4.center,stan4.U)+Environment.NewLine;
            }
            raport+="Stan po "+iters+" iteracjach"+Environment.NewLine+KSrednie.Raport(punkty,srodki,u);
            txtRaport.Text=raport;
            daneKoniec=KSrednie.Dane(punkty,srodki,u);
            if(iters>4){
                dane4=KSrednie.Dane(punkty,stan4.center,stan4.U);
            }else{
                dane4="";
            }
            //int[] u =KSrednie.Przypisz(punkty,srodki,test);
            //lblWynik.Text="Punkt 1-> grupa "+(u[0]+1);       
            //KSrednie.PrzeliczSrodki(punkty,srodki,u);
            //lblWynik.Text="Środek 1 po przeliczeniu: x1="+srodki[0][0]+" x2="+srodki[0][1];
            //Stan zapis;
            //int iteracje;
            //bool zbiezny;
            //int[] u=KSrednie.Uruchom(punkty,srodki,iters,test,5,out zapis,out iteracje,out zbiezny);
            //lblWynik.Text="Iteracje: "+iteracje+", zbieżny: "+zbiezny+", środek 1: x1="+srodki[0][0]+" x2="+srodki[0][1];
            //lblWynik.Text=KSrednie.Raport(punkty,srodki,u);
        }catch(Exception ee){
            lblWynik.Text="Błąd: "+ee.Message;
        }
    }
    void BrowseClick(object? sender, EventArgs e){
        OpenFileDialog OpenFileDialog1=new OpenFileDialog{
            Title="Szukaj",
            CheckFileExists=true,
            CheckPathExists=true,
            DefaultExt="csv",
            Filter="Pliki csv(*.csv)|*.csv|Wszystkie pliki(*.*)|*.*",
            FilterIndex=1,
            RestoreDirectory=true,
            ReadOnlyChecked=true,
            ShowReadOnly=true,
        };
        if(OpenFileDialog1.ShowDialog()==DialogResult.OK){
            sciezka=OpenFileDialog1.FileName;
        }
    }
    void Kopiuj4_Click(object? sender, EventArgs e){
        if(dane4==""){
            MessageBox.Show("Brak danych ");
            return;
        }
        Clipboard.SetText(dane4);
        MessageBox.Show("Skopiowano");
    }
    void KopiujKoniec_Click(object? sender, EventArgs e){
        if(daneKoniec==""){
            MessageBox.Show("Kliknij start");
            return;
        }
        Clipboard.SetText(daneKoniec);
        MessageBox.Show("Skopiowano");
    }
    List<List<double>> WczytajCsv(string plik){
        List<List<double>> punkty =new List<List<double>>();
        string[] linie =File.ReadAllLines(plik);
        foreach(string linia in linie){
            if(linia.Trim()==""){
                continue;
            }
            string[]czesci=linia.Split(',',';');
            List<double>punkt=new List<double>();
            foreach(string czesc in czesci){
                punkt.Add(double.Parse(czesc.Trim(),CultureInfo.InvariantCulture));
            }
            punkty.Add(punkt);
        }
        return punkty;
    }
    void ZaladujMetody(){
        foreach(var m in typeof(Metody).GetMethods(BindingFlags.Public|BindingFlags.Static).Where(m=>m.ReturnType==typeof(double))){
            comboMetody.Items.Add(m.Name);
        }
        comboMetody.SelectedIndex=0;
    }
    
    
}
