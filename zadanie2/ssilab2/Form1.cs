namespace ssilab2;

public partial class Form1 : Form
{
    TextBox txtRaport;
    public Form1()
    {
        InitializeComponent();
        Text="Algorytm 1+1";
        Size=new Size(500,666);
        Button btnStart=new Button{Text="Start",Left=30,Top=190,Width=90,Height=30};
        btnStart.Click+=btnStart_Click;
        txtRaport =new TextBox{Left=30,Top=240,Width=430,Height=350,Multiline=true,ScrollBars=ScrollBars.Vertical};
        Controls.Add(btnStart);
        Controls.Add(txtRaport);
    }
    void btnStart_Click(object? sender, EventArgs e){
        txtRaport.Text="click";
    }
}
