namespace ssilab2;

public partial class Form1 : Form
{
    TextBox txtRaport;
    NumericUpDown numRozrzut;
    NumericUpDown numWsp;
    NumericUpDown numIter;
    NumericUpDown numZakresOd;
    NumericUpDown numZakresDo;
    NumericUpDown numStartDo;
    NumericUpDown numStartOd;
    Random losuj = new Random();
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

        Label lblZakres = new Label{Text="Zakres zmienności x:",Left=30,Top=120,AutoSize=true};
        numZakresOd=new NumericUpDown{Left=220,Top=118,Width=60,Minimum=-1000, Maximum=1000,Value=0};
        Label lblZakresDo=new Label{Text="do",Left=285,Top=120,Width=20};
        numZakresDo=new NumericUpDown{Left=310,Top=118,Width=60,Minimum=-1000,Maximum=1000,Value=100};

        Label lblStart= new Label{Text="Losowanie startu x:", Left=30,Top=150,AutoSize=true};
        numStartOd=new NumericUpDown{Left=220,Top=148,Width=60,Minimum=-1000,Maximum=1000,Value=0};
        Label lblStartDo=new Label{Text="do", Left=285,Top=150,Width=20};
        numStartDo=new NumericUpDown{Left=310,Top=148,Width=60,Minimum=-1000,Maximum=1000,Value=100};



        Controls.Add(lblRozrzut);
        Controls.Add(numRozrzut);
        Controls.Add(lblWsp);
        Controls.Add(numWsp);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(lblZakres);
        Controls.Add(numZakresOd);
        Controls.Add(lblZakresDo);
        Controls.Add(numZakresDo);
        Controls.Add(lblStart);
        Controls.Add(numStartOd);
        Controls.Add(lblStartDo);
        Controls.Add(numStartDo);
        Controls.Add(btnStart);
        Controls.Add(txtRaport);
    }
    void btnStart_Click(object? sender, EventArgs e){
        double rozrzut=(double)numRozrzut.Value;
        double wsp=(double)numWsp.Value;
        double zakresOd=(double)numZakresOd.Value;
        double zakresDo=(double)numZakresDo.Value;
        double startOd=(double)numStartOd.Value;
        double startDo=(double)numStartDo.Value;


        int iteracje=(int)numIter.Value;
        double x=startOd+losuj.NextDouble()*(startDo-startOd);
        double y=Algorytm11.oblicz(x);
        txtRaport.Text+=Environment.NewLine+"Start: x ="+x+", y = "+y;

        for(int i=1;i<=iteracje;i++){
            double xpot=x+(losuj.NextDouble()*2-1)*rozrzut;
            if(xpot<zakresOd){
                xpot=zakresOd;
            }
            if(xpot>zakresDo){
                xpot=zakresDo;
            }
            double ypot=Algorytm11.oblicz(xpot);
            if(ypot>=y){
                x=xpot;
                y=ypot;
                rozrzut=rozrzut*wsp;
            }
            else{
                rozrzut=rozrzut/wsp;
            }
        }
        txtRaport.Text+=Environment.NewLine+"Koniec: x ="+x+", y= "+y+" rozrzut="+rozrzut;
    }
}
