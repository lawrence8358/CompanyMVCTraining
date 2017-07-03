using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _20170703MVC.Models.InputValidations
{
    public class ValidateTaiwanSIDAttribute : DataTypeAttribute
    {
        public ValidateTaiwanSIDAttribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            return IsIDCardNumber((string)value);
        }

        private bool IsIDCardNumber(string strIDCareNumber)
        {
            int[] A1 = new int[26] { 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3 };//辨別英文代碼第一碼數字
            int[] A2 = new int[26] { 0, 1, 2, 3, 4, 5, 6, 7, 4, 8, 9, 0, 1, 2, 5, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3 };//辨別英文代碼第二碼數字
            int[] Num = new int[10];
            string Bigtxt = strIDCareNumber.ToUpper();//轉換成大寫
            string first = Bigtxt.Substring(0, 1);//取得身分證字號第一碼

            if (strIDCareNumber.Length != 10) return false;
            //if (Bigtxt == "A123456789") return false;
            if (Char.Parse(first) < 'A' || Char.Parse(first) > 'Z') return false;

            Decimal oo = new Decimal();

            for (int i = 1; i <= 9; i++)
            {
                if (Decimal.TryParse(Bigtxt.Substring(i, 1), out oo) == false)
                //if (Char.Parse(Bigtxt.Substring(i, 1)) < '0' || Char.Parse(Bigtxt.Substring(i, 1)) > '9')
                {
                    return false;
                }
            }

            //將輸入的英文代碼轉成ASCII與A的ASCII碼相減所得數字存入陣列0
            Num[0] = System.Convert.ToInt32(Char.Parse(first)) - System.Convert.ToInt32('A');
            //依序將數字存入陣列1~9
            for (int i = 1; i <= 9; i++)
            {
                Num[i] = System.Convert.ToInt32(Char.Parse(Bigtxt.Substring(i, 1))) - System.Convert.ToInt32('0');
            }
            //身份證驗證公式
            int ANS = A1[Num[0]] + A2[Num[0]] * 9 + Num[1] * 8 + Num[2] * 7 + Num[3] * 6 + Num[4] * 5 + Num[5] * 4 + Num[6] * 3 + Num[7] * 2 + Num[8] * 1;

            int mod = (ANS % 10);
            int verifyCode = 10 - mod;

            if (mod == 0 && Num[9] == 0) return true; //需判斷最後一個邏輯，若餘數為0則驗證碼也為0
            if (verifyCode != Num[9]) return false;

            return true;
        }
    }
}