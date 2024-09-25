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
    public interface ISuitableObjectService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(int id, SuitableObjectDTO obn);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save( SuitableObjectDTO obj);
    }
    public class SuitableObjectService : ISuitableObjectService
    {
        private readonly UnitOfWork _unitOfWork;


        public SuitableObjectService()
        {
            _unitOfWork ??= new UnitOfWork();

        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {

                var obj = await _unitOfWork.SuitableObjectRepository.GetByIdAsync(id);
                if (obj != null)
                {

                    var result = await _unitOfWork.SuitableObjectRepository.RemoveAsync(obj);
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

                var objs = await _unitOfWork.SuitableObjectRepository.GetAllAsync();

                if (objs == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, objs);
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


                var obj = await _unitOfWork.SuitableObjectRepository.GetByIdAsync(id);

                if (obj == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, obj);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


        public async Task<IBusinessResult> Save(SuitableObjectDTO suitableObjectDto)
        {
            try
            {
                var newSuitableObject = new SuitableObject
                {
                    ElementId = suitableObjectDto.ElementId,
                    TypeId = suitableObjectDto.TypeId,
                    Color = suitableObjectDto.Color,
                    Size = suitableObjectDto.Size,
                    Direction = suitableObjectDto.Direction,
                    Position = suitableObjectDto.Position,
                    Shape = suitableObjectDto.Shape,
                    Volume = suitableObjectDto.Volume,
                    WaterQuality = suitableObjectDto.WaterQuality,
                    WaterTemperature = suitableObjectDto.WaterTemperature,
                    InformationDirection = suitableObjectDto.InformationDirection,
                    Flag = suitableObjectDto.Flag
                };

                int result = await _unitOfWork.SuitableObjectRepository.CreateAsync(newSuitableObject);
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


        public async Task<IBusinessResult> Update(int id, SuitableObjectDTO suitableObjectDto)
        {
            try
            {
                // Fetch the existing SuitableObject by id
                var existedSuitableObject = await _unitOfWork.SuitableObjectRepository.GetByIdAsync(id);


                // Update the properties
                existedSuitableObject.ElementId = suitableObjectDto.ElementId;
                existedSuitableObject.TypeId = suitableObjectDto.TypeId;
                existedSuitableObject.Color = suitableObjectDto.Color;
                existedSuitableObject.Size = suitableObjectDto.Size;
                existedSuitableObject.Direction = suitableObjectDto.Direction;
                existedSuitableObject.Position = suitableObjectDto.Position;
                existedSuitableObject.Shape = suitableObjectDto.Shape;
                existedSuitableObject.Volume = suitableObjectDto.Volume;
                existedSuitableObject.WaterQuality = suitableObjectDto.WaterQuality;
                existedSuitableObject.WaterTemperature = suitableObjectDto.WaterTemperature;
                existedSuitableObject.InformationDirection = suitableObjectDto.InformationDirection;
                existedSuitableObject.Flag = suitableObjectDto.Flag;

                // Update the record in the repository
                int result = await _unitOfWork.SuitableObjectRepository.UpdateAsync(existedSuitableObject);
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
