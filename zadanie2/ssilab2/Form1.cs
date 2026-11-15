namespace ssilab2;

public partial class Form1 : Form
{
    TextBox txtRaport;
    NumericUpDown numRozrzut;
    NumericUpDown numWsp;
    NumericUpDown numIter;
    public Form1()
    {
        InitializeComponent();
        Text="Algorytm 1+1";
        Size=new Size(500,666);
        Button btnStart=new Button{Text="Start",Left=30,Top=190,Width=90,Height=30};
        btnStart.Click+=btnStart_Click;
        txtRaport =new TextBox{Left=30,Top=240,Width=430,Height=350,Multiline=true,ScrollBars=ScrollBars.Vertical};

        Label lblRozrzut=new Label{Text="Rozrzut:",Left=30,Top=30,AutoSize=true};
        numRozrzut=new NumericUpDown{Left=220,Top=28,Width=80,DecimalPlaces=2,Minimum=0.01m,Maximum =100,Value=10};
        Label lblWsp=new Label{Text="Współczynnik przyrostu",Left=30,Top=60,AutoSize=true};
        numWsp=new NumericUpDown{Left=220,Top=58,Width=80,DecimalPlaces=2,Increment=0.01m,Minimum=1.01m,Maximum=10,Value=1.1m};
        Label lblIter = new Label{Text="Liczba iteracji:",Left=30,Top=90,AutoSize=true};
        numIter=new NumericUpDown{Left=220,Top=88,Width=80,Minimum=1,Maximum=1000,Value=100};

        Controls.Add(lblRozrzut);
        Controls.Add(numRozrzut);
        Controls.Add(lblWsp);
        Controls.Add(numWsp);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(btnStart);
        Controls.Add(txtRaport);
    }
    void btnStart_Click(object? sender, EventArgs e){
        double rozrzut=(double)numRozrzut.Value;
        double wsp=(double)numWsp.Value;
        int iteracje=(int)numIter.Value;
        txtRaport.Text="Rozrzut: "+rozrzut+Environment.NewLine +"Współczynnik przyrostu:  "+ wsp +Environment.NewLine + "Liczba iteracji:"+iteracje;
        
    }
}
