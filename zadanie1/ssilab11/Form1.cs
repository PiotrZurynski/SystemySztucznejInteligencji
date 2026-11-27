namespace ssilab11;

public partial class Form1 : Form
{
    string sciezka="";
    NumericUpDown numGrupy;
    NumericUpDown numIter;
    NumericUpDown numFcm;
    public Form1()
    {
        InitializeComponent();
        Text="Fuzzy c-means";
        Size=new Size(500,666);
        var lblDane=new Label{Text="Dane",Left=30,Top=30,AutoSize=true};
        var btnSzukaj=new Button{Text="Szukaj",Left=75,Top=30,Width=100,Height=30};
        btnSzukaj.Click+=BrowseClick;
        var lblGrupy = new Label{Text="Liczba grup:",Left=30,Top=75,AutoSize=true};
        numGrupy=new NumericUpDown{Left=140,Top=73,Width=60,Minimum=2,Maximum=100,Value=3};
        var lblIter=new Label{Text="Liczba iteracji",Left=30,Top=105,AutoSize=true};
        numIter=new NumericUpDown{Left=140,Top=103,Width=60,Minimum=1,Maximum=1000,Value=10};
        var lblFcm=new Label{Text="fcm_m:",Left=30,Top=135,AutoSize=true};
        numFcm = new NumericUpDown{Left=140,Top=133,Width=60,DecimalPlaces=2,Increment=0.1m,Minimum=1.01m,Maximum=10,Value=2};




        Controls.Add(lblDane);
        Controls.Add(btnSzukaj);
        Controls.Add(lblGrupy);
        Controls.Add(numGrupy);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(lblFcm);
        Controls.Add(numFcm);
        

    }
    void BrowseClick(object? sender, EventArgs e){
        OpenFileDialog okno=new OpenFileDialog{
            Title="Szukaj",
            CheckFileExists=true,
            Filter="Pliki csv(*.csv)|*.csv|Wszystkie pliki(*.*)|*.*",};
            if(okno.ShowDialog()==DialogResult.OK){
                sciezka=okno.FileName;
            }
        }
}

