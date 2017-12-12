using System;
using System.Collections.Generic;
using System.Text;

namespace ShebaConverter
{
    public class ShebaUtility
    {
        #region Variables
        private string _ShebaNumber;
        private string _AccountNumber;


        public string ShebaNumber
        {
            get
            {
                return _ShebaNumber;
            }
        }
        public string AccountNumber
        {
            get
            {
                return _AccountNumber;
            }
        }
        public bool isValidSheba
        {
            get
            {
                return CheckShebaValidation();
            }
        }
        public bool hasError
        {
            get
            {
                return _Exception == null ? false : true;
            }
        }
        public string BankCode
        {
            get
            {
                if (CheckShebaValidation()) return _ShebaNumber.Substring(4, 3);
                return "";
            }
        }
        public BankCodeEnum bankCodeEnum
        {
            get
            {
                return getBankEnum();
            }
        }
        private Exception _Exception;
        public Exception Exception
        {
            get { return _Exception; }
        }

        #endregion
        #region Constructor
        public ShebaUtility(string shebaNumber)
        {
            _ShebaNumber = shebaNumber;
        }

        public ShebaUtility(string shebaNumber, string accountNumber)
        {
            _AccountNumber = accountNumber;
        }
        public ShebaUtility(string accountNumber, bool chkAcc)
        {
            _AccountNumber = accountNumber;
        }
        #endregion
        #region Methods
        public bool CheckShebaValidation()
        {
            try
            {
                #region NotEmpty
                if (string.IsNullOrEmpty(_ShebaNumber) || string.IsNullOrWhiteSpace(_ShebaNumber))
                    throw new Exception("شماره شبا مقدار دهی نشده است");

                #endregion
                #region Length && ir check
                if (_ShebaNumber.Length != 26 || (_ShebaNumber.Length > 2 && _ShebaNumber.Substring(0, 2).ToLower() != "ir"))
                    throw new Exception("طول شماره شبا 26 کاراکتر می باشد و می بایست با IR شروع شود");

                #endregion
                #region FormulaExpr

                string res = _ShebaNumber.ToUpper();
                string fourChar = res.Substring(0, 4);
                res = res.Substring(4, 22) + fourChar;
                res = res.Replace("A", "10");
                res = res.Replace("B", "11");
                res = res.Replace("C", "12");
                res = res.Replace("D", "13");
                res = res.Replace("E", "14");
                res = res.Replace("F", "15");
                res = res.Replace("G", "16");
                res = res.Replace("H", "17");
                res = res.Replace("I", "18");
                res = res.Replace("J", "19");
                res = res.Replace("K", "20");
                res = res.Replace("L", "21");
                res = res.Replace("M", "22");
                res = res.Replace("N", "23");
                res = res.Replace("O", "24");
                res = res.Replace("P", "25");
                res = res.Replace("Q", "26");
                res = res.Replace("R", "27");
                res = res.Replace("S", "28");
                res = res.Replace("T", "29");
                res = res.Replace("U", "30");
                res = res.Replace("V", "31");
                res = res.Replace("W", "32");
                res = res.Replace("X", "33");
                res = res.Replace("Y", "34");
                res = res.Replace("Z", "35");
                decimal resNum = Convert.ToDecimal(res);
                decimal re = resNum % 97;
                if (re != 1)
                {
                    throw new Exception("شماره شبا وارد شده اشتباه می باشد");
                }
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                _Exception = ex;
                return false;
            }

        }
        public void ConvertShebaToAccount()
        {
            try
            {
                if (!CheckShebaValidation())
                    throw _Exception;
                _AccountNumber = _ShebaNumber.Substring(7, 19);

            }
            catch (Exception ex)
            {
                _Exception = ex;
                return;
            }
        }

        public void ConvertAccountToSheba(BankCodeEnum bank)
        {
            try
            {
                if (string.IsNullOrEmpty(_AccountNumber) || string.IsNullOrWhiteSpace(_AccountNumber))
                    throw new Exception("شماره حساب مقدار ندارد");
                string sheba = "0";
                sheba += Convert.ToInt32(bank).ToString();

                for (int i = _AccountNumber.Length; i < 19; i++)
                {
                    sheba += "0";
                }
                sheba += _AccountNumber + "182700";
                decimal numSheba = Convert.ToDecimal(sheba);
                decimal re = numSheba % 97;
                int chk = Convert.ToInt32(98 - re);
                _ShebaNumber = "IR" + ((chk <= 9) ? "0" + chk.ToString() : chk.ToString()) + sheba.Substring(0, 22);
            }
            catch (Exception ex)
            {
                _Exception = ex;
                return;
            }

        }
        private BankCodeEnum getBankEnum()
        {
            try
            {
                BankCodeEnum _enum = BankCodeEnum.Noor;
                string re = Convert.ToInt32(BankCode).ToString();
                if (!Enum.TryParse(re, out _enum))
                    throw new Exception("کد بانک قابل محاسبه نمی باشد");
                return (BankCodeEnum)_enum;
            }
            catch (Exception ex)
            {
                _Exception = ex;
                return BankCodeEnum.Noor;
            }

        }


        public static string ConvertAccountToSheba(string account, string bankCode)
        {
            try
            {

                if (string.IsNullOrEmpty(account) || string.IsNullOrWhiteSpace(account))
                    throw new Exception("شماره حساب مقدار ندارد");
                string sheba = "0";
                sheba += bankCode;

                for (int i = account.Length; i < 19; i++)
                {
                    sheba += "0";
                }
                sheba += account + "182700";
                decimal numSheba = Convert.ToDecimal(sheba);
                decimal re = numSheba % 97;
                int chk = Convert.ToInt32(98 - re);
                return "IR" + ((chk <= 9) ? "0" + chk.ToString() : chk.ToString()) + sheba.Substring(0, 22);
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        #endregion

    }
}
