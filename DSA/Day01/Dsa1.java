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

    // 1/6/2026
    public static int reverseNum(int n){
        int num=n;
        int rev=0;
        while(num>0){
            int d=num%10;
            rev=rev*10+d;
            num=num/10;
        }
        return rev;
    }

    // 20-6-2026
    public static int rotateNum(int n){
        // Decimal number rotation
        if(n==0) return 0;
        
        int len=(int)Math.log10(Math.abs(n))+1;
        // Math.log10 always gives a pattern 
        // Math.log10(1) = 0  Math.log10(10) = 1 Math.log10(100) = 2 Math.log10(1000) = 3
        k=k%len;
  
        if(k<0){
           k=k+len;
        }
        int d1=(int)Math.pow(10,k);
        int d2=(int)Math.pow(10,len-k);
        int left=n/d1;
        int right=n%d1;
        int rev=right*d2+left;
        return rev;
    }
    
  }
}