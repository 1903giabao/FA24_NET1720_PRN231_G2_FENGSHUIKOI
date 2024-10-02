using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Enums;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Data.UnitOfWork;
using FENGSHUIKOI.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Service.Services
{
    public interface IElementService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(Element element);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(Element element);
        Task<IBusinessResult> CaculateElement(string gender, int year);
    }
    public class ElementService : IElementService
    {
        private readonly UnitOfWork _unitOfWork;


        public ElementService()
        {
            _unitOfWork ??= new UnitOfWork(); 

        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                
                var payment = await _unitOfWork.ElementRepository.GetByIdAsync(id);
                if (payment != null)
                {
        
                    var result = await _unitOfWork.ElementRepository.RemoveAsync(payment);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                var payments = await _unitOfWork.ElementRepository.GetAllAsync();

                if (payments == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, payments);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                #region Business rule
                #endregion


                var payment = await _unitOfWork.ElementRepository.GetByIdAsync(id);

                if (payment == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, payment);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

       
        public async Task<IBusinessResult> Save(Element payment)
        {
            try
            {
                int result = await _unitOfWork.ElementRepository.CreateAsync(payment); 
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Update(Element payment)
        {
            try
            {

                int result = await _unitOfWork.ElementRepository.UpdateAsync(payment);
                if (result > 0)
                {
                    return new BusinessResult(Const.FAIL_UDATE, Const.SUCCESS_UDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UDATE, Const.FAIL_UDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        public async Task<string> CaculateThienCan(int year)
        {
            var thienCanValue = year % 9;
            var thienCan = ElementObjects.ThienCan.GetValueOrDefault(thienCanValue);
            return thienCan;
        }

        public async Task<string> CaculateDiaChi(int year)
        {
            var diaChiValue = year % 12;
            var diaChi = ElementObjects.DiaChi.GetValueOrDefault(diaChiValue);
            return diaChi;
        }

        public async Task<string> CaculateCungMenh(int year, string gender)
        {
            var normalizedGender = gender.ToLower();

            var previousElement = normalizedGender == "male"
                ? ElementObjects.MaleValueInElement.GetValueOrDefault(year % 9)
                : ElementObjects.FemaleValueInElement.GetValueOrDefault(year % 9);
            return previousElement;
        }


        public async Task<IBusinessResult> CaculateElement(string gender, int year)
        {
            try
            {

                if (gender.ToLower() != "male" && gender.ToLower() != "female") return new BusinessResult(400, "The gender is not suitable.");
                if (year < 0) return new BusinessResult(400, "The year is not suitable.");

                var normalizedGender = gender.ToLower();

                var previousElement = normalizedGender == "male"
                    ? ElementObjects.MaleValueInElement.GetValueOrDefault(year % 9)
                    : ElementObjects.FemaleValueInElement.GetValueOrDefault(year % 9);

                var thienCanValue = year % 9;
                var diaChiValue = year % 12;

                var thienCan = ElementObjects.ThienCan.GetValueOrDefault(thienCanValue);
                var diaChi = ElementObjects.DiaChi.GetValueOrDefault(diaChiValue);

                var elementValue = diaChiValue + thienCanValue;
                if (elementValue > 5) elementValue -= 5; 

                var element = ElementObjects.ElementValue.GetValueOrDefault(elementValue);

                return new BusinessResult(200,"Caculating element is success" , new
                {
                    ThienCan = thienCan,
                    DiaChi = diaChi,
                    Menh = previousElement + " " + element,
                });

            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}
