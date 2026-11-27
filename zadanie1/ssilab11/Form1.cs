namespace ssilab11;

public partial class Form1 : Form
{
    string sciezka="";
    NumericUpDown numGrupy;
    NumericUpDown numIter;
    NumericUpDown numFcm;
    TextBox txtRaport;
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
        var btnStart=new Button{Text="Start",Left=30,Top=175,Width=90,Height=30};
        btnStart.Click+=btnStart_Click;
        txtRaport=new TextBox{Left=30,Top=220,Width=430,Height=370,Multiline=true,ScrollBars=ScrollBars.Vertical};




        Controls.Add(lblDane);
        Controls.Add(btnSzukaj);
        Controls.Add(lblGrupy);
        Controls.Add(numGrupy);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(lblFcm);
        Controls.Add(numFcm);
        Controls.Add(btnStart);
        Controls.Add(txtRaport);

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
    void btnStart_Click(object? sender,EventArgs e){
        if(sciezka==""){
            MessageBox.Show("Wybierz plik");
            return;
        }
        int m=(int)numGrupy.Value;
        int iteracje=(int)numIter.Value;
        double fcm_m=(double)numFcm.Value;
        txtRaport.Text="plik="+sciezka+Environment.NewLine;
        txtRaport.Text+="grupy="+m+" iteracje="+iteracje+" fcm_m="+fcm_m+Environment.NewLine;

    }
}

