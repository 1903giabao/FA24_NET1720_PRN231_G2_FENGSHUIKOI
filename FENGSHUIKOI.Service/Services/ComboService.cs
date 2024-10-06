using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Data.UnitOfWork;
using FENGSHUIKOI.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Service.Services
{
    public interface IComboService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(int id, ComboRequestDTO combo);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(ComboRequestDTO combo);
    }

    public class ComboService : IComboService
    {
        private readonly UnitOfWork _unitOfWork;

        public ComboService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {

                var obj = await _unitOfWork.ComboRepository.GetByIdAsync(id);
                if (obj != null)
                {

                    var result = await _unitOfWork.ComboRepository.RemoveAsync(obj);
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

                var combos = await _unitOfWork.ComboRepository.GetAllAsync();

                if (combos == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, combos);
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


                var combos = await _unitOfWork.ComboRepository.GetByIdAsync(id);

                if (combos == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, combos);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


        public async Task<IBusinessResult> Save(ComboRequestDTO combo)
        {
            try
            {
                var newCombo = new Combo
                {
                    MemberId = combo.MemberId,
                    ElementId = combo.ElementId,
                    ProductDetailId = combo.ProductDetailId,
                    ComboName = combo.ComboName,
                    ComboPrice = combo.ComboPrice,
                    Discount = combo.Discount,
                    Status = combo.Status,
                    CreatedBy = combo.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                int result = await _unitOfWork.ComboRepository.CreateAsync(newCombo);
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

        public async Task<IBusinessResult> Update(int id, ComboRequestDTO combo)
        {
            try
            {
                var existedCombo = await _unitOfWork.ComboRepository.GetByIdAsync(id);
                
                if(existedCombo != null)
                {
                    existedCombo.MemberId = combo.MemberId;
                    existedCombo.ElementId = combo.ElementId;
                    existedCombo.ProductDetailId = combo.ProductDetailId;
                    existedCombo.ComboName = combo.ComboName;
                    existedCombo.ComboPrice = combo.ComboPrice;
                    existedCombo.Discount = combo.Discount;
                    existedCombo.Status = combo.Status;
                    existedCombo.UpdatedAt = DateTime.Now;
                } else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                int result = await _unitOfWork.ComboRepository.UpdateAsync(existedCombo);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UDATE, Const.SUCCESS_UDATE_MSG);
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
    }
}
