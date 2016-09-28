using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Compiler.Generator
{
    public static class GenerateMain
    {
        public static string ClassDefinitionCode = "";
        public static string InitJavaCode(string code)
        {
            var sourceCode = @"import java.util.*;
                                "+ClassDefinitionCode+@"
                                public class MyClass {
                                
                                public static String MultiplyString(int count, String str){
                                    String temp = """";
                                    for(int i = 0; i < count; i++) {
                                        temp += str;
                                    }
                                    return temp;
                                }
            
                                public static void main(String args[]) {
                                    Scanner lea = new Scanner(System.in);
                                    " + code + @"
                                
                                    }
                                }
                                ";

            return sourceCode;
        }

    }
}
