namespace ssilab22;

public partial class Form1 : Form
{   
    NumericUpDown numN;
    NumericUpDown numInercja;
    NumericUpDown numGlob;
    NumericUpDown numLok;
    NumericUpDown numIter;

    NumericUpDown numOd;
    NumericUpDown numDo;
    TextBox txtRaport;
    Random losuj=new Random();
    public Form1()
    {
        InitializeComponent();
        Text="PSO";
        Size=new Size(500,666);
        Label lblN=new Label{Text="Liczba osobników N:", Left=30,Top=30,AutoSize=true};
        numN = new NumericUpDown{Left=220,Top=28,Width=80,Minimum=2,Maximum=1000,Value=4};
        Label lblInercja=new Label{Text="rinercji:",Left=30,Top=60,AutoSize=true};
        numInercja=new NumericUpDown{Left=220,Top=58,Width=80,DecimalPlaces=2,Increment=0.05m,Minimum=0,Maximum=1,Value=0.2m};
        Label lblGlob=new Label{Text="rglob",Left=30,Top=90,AutoSize=true};
        numGlob=new NumericUpDown{Left=220,Top=88,Width=88,DecimalPlaces=2,Increment=0.05m,Minimum=0,Maximum=1, Value=0.3m};
        Label lblLok=new Label{Text="rlok",Left=30,Top=120,AutoSize=true};
        numLok=new NumericUpDown{Left=220,Top=118,Width=80,DecimalPlaces=2,Increment=0.05m,Minimum=0,Maximum=1,Value=0.3m};
        Label lblIter=new Label{Text="Liczba iteracji:",Left=30,Top=150,AutoSize=true};
        numIter=new NumericUpDown{Left=220,Top=148,Width=80,Minimum=1,Maximum=1000,Value=30};

        Label lblZakres=new Label{Text="Zakres x od /do",Left=30,Top=180,AutoSize=true};
        numOd=new NumericUpDown{Left=220,Top=178,Width=80,Minimum=-1000,Maximum=1000,Value=0};
        numDo=new NumericUpDown{Left=310,Top=178,Width=80,Minimum=-1000,Maximum=1000,Value=100};
        Button btnStart=new Button{Text="Start",Left=30,Top=220,Width=90,Height=30};
        btnStart.Click+=btnStart_Click;
        txtRaport=new TextBox{Left=30,Top=270,Width=430,Height=320,Multiline=true,ScrollBars=ScrollBars.Vertical};



        Controls.Add(lblN);
        Controls.Add(numN);
        Controls.Add(lblInercja);
        Controls.Add(numInercja);
        Controls.Add(lblGlob);
        Controls.Add(numGlob);
        Controls.Add(lblLok);
        Controls.Add(numLok);
        Controls.Add(lblIter);
        Controls.Add(numIter);
        Controls.Add(lblZakres);
        Controls.Add(numOd);
        Controls.Add(numDo);
        Controls.Add(btnStart);
        Controls.Add(txtRaport);

    }
    void btnStart_Click(object? sender, EventArgs e){
        int n=(int)numN.Value;
        double rinercji=(double)numInercja.Value;
        double rglob=(double)numGlob.Value;
        double rlok=(double)numLok.Value;
        int iteracje=(int)numIter.Value;
        double xod=(double)numOd.Value;
        double xdo=(double)numDo.Value;
        if(xod>=xdo){
            MessageBox.Show("Zakres musi być mniejszy niż 'do' ");
            return;
        }
        txtRaport.Text="N="+n+" rinercji="+rinercji+" rglob="+rglob+" rlok="+rlok+Environment.NewLine;
        txtRaport.Text+="iteracje="+iteracje+" zakres od "+xod+" do "+xdo+Environment.NewLine;
        //txtRaport.Text+="F(0,0)="+PSO.oblicz(0,0)+Environment.NewLine;
        //txtRaport.Text+="F(50,50)="+PSO.oblicz(50,50)+Environment.NewLine;
        //txtRaport.Text+="F(10,20)="+PSO.oblicz(10,20)+Environment.NewLine;
        double[] x1=new double[n];
        double[] x2=new double[n];
        double[] lok1=new double[n];
        double[] lok2=new double[n];
        double[] v1=new double[n];
        double[] v2=new double[n];
        for(int j=0;j<n;j++){
            x1[j]=xod+losuj.NextDouble()*(xdo-xod);
            x2[j]=xod+losuj.NextDouble()*(xdo-xod);
            lok1[j]=x1[j];
            lok2[j]=x2[j];
            v1[j]=0;
            v2[j]=0;
        }
        double[] ocena=new double[n];
        double[] flok = new double[n];
        int najlepszy=0;
        int najgorszy=0;
        for(int j=0;j<n;j++){
            ocena[j]=PSO.oblicz(x1[j],x2[j]);
            flok[j]=ocena[j];
            if(ocena[j]>ocena[najlepszy]){
                najlepszy=j;
            }
            if(ocena[j]<ocena[najgorszy]){
                najgorszy=j;
            }
        }
        double glob1=x1[najlepszy];
        double glob2=x2[najlepszy];
        double fglob=ocena[najlepszy];
        txtRaport.Text+=Environment.NewLine+"Iteracja 0"+Environment.NewLine;
        txtRaport.Text+="osobnik\tx1\tx2\tF"+Environment.NewLine;
        for(int j=0;j<n;j++){
            txtRaport.Text+=j+"\t"+x1[j]+"\t"+x2[j]+"\t"+ocena[j]+Environment.NewLine;

        }
        txtRaport.Text+="Najlepszy osobnik"+najlepszy+" x1="+x1[najlepszy]+" x2="+x2[najlepszy]+" F="+ocena[najlepszy]+Environment.NewLine;
        txtRaport.Text+="Najgorszy osobnik"+najgorszy+" x1="+x1[najgorszy]+" x2="+x2[najgorszy]+" F="+ocena[najgorszy]+Environment.NewLine;
        txtRaport.Text+="xglob: x1="+glob1+" x2="+glob2+" F="+fglob+Environment.NewLine;
    for(int it=1;it<=iteracje;it++){
        for(int j=0;j<n;j++){
            double rndGlob=losuj.NextDouble();
            double rndLok=losuj.NextDouble();
            v1[j]=v1[j]*rinercji+(glob1-x1[j])*rglob*rndGlob+(lok1[j]-x1[j])*rlok*rndLok;
            v2[j]=v2[j]*rinercji+(glob2-x2[j])*rglob*rndGlob+(lok2[j]-x2[j])*rlok*rndLok;
            x1[j]=x1[j]+v1[j];
            x2[j]=x2[j]+v2[j];
        }
        for(int j=0;j<n;j++){
            ocena[j]=PSO.oblicz(x1[j],x2[j]);
            if(ocena[j]>flok[j]){
                flok[j]=ocena[j];
                lok1[j]=x1[j];
                lok2[j]=x2[j];
            }
            if(ocena[j]>fglob){
                fglob=ocena[j];
                glob1=x1[j];
                glob2=x2[j];
            }
        }

        najlepszy=0;
        najgorszy=0;
        for(int j=1;j<n;j++){
            if(ocena[j]>ocena[najlepszy]){
                najlepszy=j;
            }
            if(ocena[j]<ocena[najgorszy]){
                najgorszy=j;
            }
        }
        txtRaport.Text+=Environment.NewLine+"Iteracja "+it+Environment.NewLine;
        txtRaport.Text+="osobnik\tx1\tx2\tF"+Environment.NewLine;
        for(int j=0;j<n;j++){
            txtRaport.Text+=j+"\t"+x1[j]+"\t"+x2[j]+"\t"+ocena[j]+Environment.NewLine;
        }
        txtRaport.Text+="Najlepszy osobnik "+najlepszy+" x1="+x1[najlepszy]+" x2="+x2[najlepszy]+" F="+ocena[najlepszy]+Environment.NewLine;
        txtRaport.Text+="Najgorszy osobnik "+najgorszy+" x1="+x1[najgorszy]+" x2="+x2[najgorszy]+" F="+ocena[najgorszy]+Environment.NewLine;
        txtRaport.Text+="xglob: x1="+glob1+" x2="+glob2+" F="+fglob+Environment.NewLine;
    }
    }
}
