namespace ssilab1;

public static class Metody{
    public static double Euklides(List<double> a, List<double> b){
        double suma=0;
        for(int i=0;i<a.Count;i++){
            suma+=Math.Pow(a[i]-b[i],2);
        }
        return Math.Sqrt(suma);
    }
    public static double x1(List<double> a, List<double> b){
        return Math.Abs(a[0]-b[0]);
    }
}