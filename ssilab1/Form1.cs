using System.Globalization;
namespace ssilab1;

public partial class Form1 : Form
{
    string sciezka="";
    Random rnd = new Random();
    NumericUpDown numK;
    NumericUpDown numIter;
    Label lblWynik;

    public Form1()
    {   
        InitializeComponent();
        Text="Metody";
        Size=new Size(500,500);

        var lblDane = new Label{Text="Dane",Left=30,Top=30,AutoSize=true};
        var btnSzukaj= new Button{Text="Szukaj", Left=75,Top=30,Width=100,Height=30};
        btnSzukaj.Click+=BrowseClick;
        var lblnumK = new Label{Text="k", Left=30,Top=70,Width=20};
        numK=new NumericUpDown{Left=50,Top=70,Width=50,Minimum=2,Maximum=100,Value=4};
        var lblIter = new Label{Text="Liczba iteracji:",Left=30,Top=100,AutoSize=true};
        numIter=new NumericUpDown{Left=140,Top=100,Value=10,Maximum=100,Minimum=1};
        var btnStart=new Button{Text="Start",Left=30,Top=170,Width=90,Height=30};
        btnStart.Click+=BtnOblicz_Click;
        lblWynik=new Label{Left=30,Top=200,Width=300,Height=120,Text=""};

        Controls.Add(lblDane);
        Controls.Add(btnSzukaj);
        Controls.Add(lblnumK);
        Controls.Add(numK);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(btnStart);
        Controls.Add(lblWynik);

    }
    void BtnOblicz_Click(object? sender, EventArgs e){
        try{
            List<List<double>>punkty = WczytajCsv(sciezka);
            int k=(int)numK.Value;
            int iters=(int)numIter.Value;
            var srodki=KSrednie.LosujSrodki(punkty,k,rnd);
            lblWynik.Text="Środek 1: x1="+srodki[0][0] +" x2="+srodki[0][1];
            //lblWynik.Text="Wczytano punktów: "+punkty.Count+", wymiar: "+punkty[0].Count+"\nk: "+k+", iteracji: "+iters;
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
    
    
}
