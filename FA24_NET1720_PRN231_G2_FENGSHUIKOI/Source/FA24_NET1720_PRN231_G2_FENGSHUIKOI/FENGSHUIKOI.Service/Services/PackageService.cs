using FENGSHUIKOI.Common;
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
    public interface IPackageService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(Package package);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(Package package);
        Task<IBusinessResult> SearchPackage(int? id, string? name, double? price, string? featureType, bool? highlight);
    }
    public class PackageService : IPackageService
    {
        private readonly UnitOfWork _unitOfWork;

        public PackageService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                var packages = await _unitOfWork.ElementRepository.GetAllAsync();

                if (packages == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, packages);
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
                var package = await _unitOfWork.PackageRepository.GetByIdAsync(id);
                if (package == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, package);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var package = await _unitOfWork.PackageRepository.GetByIdAsync(id);
                if (package == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                var removePackage = await _unitOfWork.PackageRepository.RemoveAsync(package);
                if (removePackage)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE, Const.SUCCESS_DELETE_MSG, removePackage);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE, Const.FAIL_DELETE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }

        }

        public async Task<IBusinessResult> Save(Package package)
        {
            try
            {

                var newPackage = await _unitOfWork.PackageRepository.CreateAsync(package);
                if (newPackage > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE, Const.SUCCESS_CREATE_MSG, newPackage);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                {
                    return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
                }
            }
        }

        public async Task<IBusinessResult> Update(Package package)
        {
            try
            {
                var packageUpdate = await _unitOfWork.PackageRepository.UpdateAsync(package);
                if (packageUpdate > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE, Const.SUCCESS_CREATE_MSG, packageUpdate);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> SearchPackage(int? id, string? name, double? price, string? featureType, bool? highlight)
        {
            try
            {
                var package = await _unitOfWork.PackageRepository.GetAllAsync();
                if (package == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                if(id.HasValue)
                {
                    package = package.Where(x => x.Id == id.Value).ToList();
                }
                if (string.IsNullOrEmpty(name))
                {
                    package = package.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
                }if (price.HasValue) 
                {
                    package = package.Where(x => x.Price == price.Value).ToList();
                }if(string.IsNullOrEmpty(featureType))
                {
                    package = package.Where(x => x.FeatureType.ToLower().Contains(featureType.ToLower())).ToList();
                }if (highlight != null)
                {
                    package = package.Where(x => x.Highlight == highlight.Value).ToList();
                }

                var packageList = package.ToList();

                if(!packageList.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, package);
                }
            }
            catch (Exception ex)
            {
                {
                    return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
                }
            }
        }
    }
}
