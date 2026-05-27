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

    // nth fibbonacci number 27-05-2026
     public int fib(int n) {
        if(n==0 || n==1) return n;
        else{
            return fib(n-1)+fib(n-2);
        }
    }

    public static int countNumberOfDigits(int n){
        int copy=n,cnt=0;
        
        while(copy>0){
            copy=copy/10;
            cnt++;
            copy=copy%10;
        }
        
        System.out.println(cnt);
        String num=String.valueOf(n);
        return num.length();
    }

    public static int reverseANum(int n){
        int rev=0;
        
        while(n>0){
            int digit=n%10;
            rev=rev*10+digit;
            n=n/10;
        }
        return rev;
    }
    
    public static ArrayList digitsOfANumber(int n){
        ArrayList arr=new ArrayList();
        
        while(n>0){
            int digit=n%10;
            arr.add(digit);
            n=n/10;
        }
        return arr;
    }
  }
}