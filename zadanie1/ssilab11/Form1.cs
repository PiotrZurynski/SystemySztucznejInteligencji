namespace ssilab11;

public partial class Form1 : Form
{
    string sciezka="";
    public Form1()
    {
        InitializeComponent();
        Text="Fuzzy c-means";
        Size=new Size(500,666);
        var lblDane=new Label{Text="Dane",Left=30,Top=30,AutoSize=true};
        var btnSzukaj=new Button{Text="Szukaj",Left=75,Top=30,Width=100,Height=30};
        btnSzukaj.Click+=BrowseClick;

        Controls.Add(lblDane);
        Controls.Add(btnSzukaj);

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

