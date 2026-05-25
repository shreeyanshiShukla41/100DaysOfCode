public class Dsa1{
  public static void main(){
    //  code 1  25-05-2026
    public static boolean isPrime(int n)
    {
        if (n<2) return true;
        
        for(int i=2;i<=Math.sqrt(n);i++){
            if(n%i==0){
                return false;
            }
        }
        return true;
    }
    
    public static ArrayList<Integer> primeLst(int s,int e){
        ArrayList<Integer> arr=new ArrayList<>();
        for(int i=s;i<=e;i++){
            if(isPrime(i)){
                arr.add(i);
            }
        }
        return arr;
    }
  }
}