using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Data.UnitOfWork;
using FENGSHUIKOI.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Service.Services
{

    public interface IProductDetailService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(int id, ProductDetailDTO productDetail);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(ProductDetailDTO productDetail);
    }

    public class ProductDetailService : IProductDetailService
    {
        private readonly UnitOfWork _unitOfWork;


        public ProductDetailService()
        {
            _unitOfWork ??= new UnitOfWork();

        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {

                var payment = await _unitOfWork.ProductDetailRepository.GetByIdAsync(id);
                if (payment != null)
                {

                    var result = await _unitOfWork.ProductDetailRepository.RemoveAsync(payment);
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

                var payments = await _unitOfWork.ProductDetailRepository.GetAllAsync();

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


                var payment = await _unitOfWork.ProductDetailRepository.GetByIdAsync(id);

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


        public async Task<IBusinessResult> Save(ProductDetailDTO productDetail)
        {
            try
            {

                var newProduct = new ProductDetail
                {
                    Name = productDetail.Name,
                    Description = productDetail.Description,
                    ComboId = productDetail.ComboId,
                    Quantity = productDetail.Quantity,
                    Type = productDetail.Type,
                    Status = true,
                    Origin = productDetail.Origin,
                    Color = productDetail.Color,
                    Size = productDetail.Size,
                    CreateDate = DateTime.Now
                };

                int result = await _unitOfWork.ProductDetailRepository.CreateAsync(newProduct);
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

        public async Task<IBusinessResult> Update(int id, ProductDetailDTO productDetail)
        {
            try
            {
                var existedProductDetail = _unitOfWork.ProductDetailRepository.GetById(id);

                existedProductDetail.Name = productDetail.Name;
                existedProductDetail.Description = productDetail.Description;
                existedProductDetail.ComboId = productDetail.ComboId;
                existedProductDetail.Quantity = productDetail.Quantity;
                existedProductDetail.Type = productDetail.Type;
                existedProductDetail.Status = true;
                existedProductDetail.Origin = productDetail.Origin;
                existedProductDetail.Color = productDetail.Color;
                existedProductDetail.Size = productDetail.Size;
                int result = await _unitOfWork.ProductDetailRepository.UpdateAsync(existedProductDetail);
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
    }
}
