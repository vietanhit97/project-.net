using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace HistoryGenPin.Common
{
    public class CommonFunc
    {
        public static string EncryptPassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public class BussinessModelMiddleApp
        {
            public bool Status { set; get; }
            public string Code { set; get; }
            public string Message { set; get; }
            public object Data { set; get; }
        }

        public static class B_ExceptionMiddleApp
        {
            public static List<BussinessModelMiddleApp> ListBusinessMess = new List<BussinessModelMiddleApp>()
            {
                new BussinessModelMiddleApp()
                {
                    Code = "M_CreateAccount_Success",
                    Message = "Thêm mới thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_EditAccount_Success",
                    Message = "Chỉnh sửa thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_EditAccount_Failed",
                    Message = "Chỉnh sửa thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_InfoAccount_Success",
                    Message = "Lấy thông tin thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_InfoAccount_Failed",
                    Message = "Lấy thông tin thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_ExistNameAccount_Success",
                    Message = "Đã tồn tại tên tài khoản"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_ExistNameAccount_Failed",
                    Message = "Chưa tồn tại tên tài khoản"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_CreateRole_Success",
                    Message = "Tạo mới quyền thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_CreateRole_Failed",
                    Message = "Tạo mới quyền thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_UpdateRole_Success",
                    Message = "Chỉnh sửa quyền thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_UpdateRole_Failed",
                    Message = "Chỉnh sửa quyền thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_DeleteRole_Success",
                    Message = "Xóa quyền thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_DeleteRole_Failed",
                    Message = "Xóa quyền thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_ChangePassword_Success",
                    Message = "Đổi mật khẩu thành công"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_ChangePassword_Failed",
                    Message = "Đổi mật khẩu thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_CreateAccount_Failed",
                    Message = "Tạo tài khoản thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_Login_Failed",
                    Message = "Đăng nhập thất bại"
                },
                new BussinessModelMiddleApp()
                {
                    Code = "M_Login_Success",
                    Message = "Đăng nhập thành công"
                }
            };

            public static BussinessModelMiddleApp ReadCode<T>(string code, bool status, T Data, params object[] pram)
            {
                foreach (BussinessModelMiddleApp item in ListBusinessMess)
                {
                    if (item.Code == code)
                    {
                        item.Status = status;
                        item.Data = Data;
                        item.Message = string.Format(item.Message, pram);
                        return item;
                    }
                }
                return new BussinessModelMiddleApp()
                {
                    Status = false,
                    Code = "S_01",
                    Message = string.Format("Không tim thấy message: {0}", code),
                    Data = null
                };
            }

            public static BussinessModelMiddleApp ReadCode<T>(string code, bool status, T Data)
            {
                foreach (BussinessModelMiddleApp item in ListBusinessMess)
                {
                    if (item.Code == code)
                    {
                        item.Status = status;
                        item.Data = Data;
                        item.Message = item.Message;
                        return item;
                    }
                }
                return new BussinessModelMiddleApp()
                {
                    Status = false,
                    Code = "S_01",
                    Message = string.Format("Không tim thấy message: {0}", code),
                    Data = null
                };
            }

            public static BussinessModelMiddleApp ReadCode(string code, bool status)
            {
                foreach (BussinessModelMiddleApp item in ListBusinessMess)
                {
                    if (item.Code == code)
                    {
                        item.Status = status;
                        item.Message = item.Message;
                        return item;
                    }
                }
                return new BussinessModelMiddleApp()
                {
                    Status = false,
                    Code = "S_01",
                    Message = string.Format("Không tim thấy message: {0}", code)
                };
            }

        }

        public static void ValidationModel(dynamic modelValidate)
        {
            ValidationContext context = new ValidationContext(modelValidate, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(modelValidate, context, results, true);

            if (!isValid)
            {
                StringBuilder sbrErrors = new StringBuilder();
                foreach (var validationResult in results)
                {
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }
                throw new ValidationException(sbrErrors.ToString());
            }
        }
    }
}
