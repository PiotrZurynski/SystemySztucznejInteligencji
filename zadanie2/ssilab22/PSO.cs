namespace ssilab22;

public class PSO{
    public static double oblicz(double x1,double x2){
        return Math.Sin(x1*0.05)+Math.Sin(x2*0.05)+0.4*Math.Sin(x1*0.15)*Math.Sin(x2*0.15);
    }
}