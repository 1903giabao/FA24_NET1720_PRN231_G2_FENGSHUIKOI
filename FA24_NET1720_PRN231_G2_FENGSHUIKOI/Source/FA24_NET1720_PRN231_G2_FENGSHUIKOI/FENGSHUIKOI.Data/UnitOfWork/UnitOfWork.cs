using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Data.UnitOfWork
{
    public class UnitOfWork
    {
        public NET1720_231_2_FENGSHUIKOIContext _unitOfWorkContext;
        private AccountRepository _account;
        private ComboRepository _combo;
        private ElementRepository _element;
        private MemberPackageRepository _memberPackage;
        private MemberRepository _member;
        private PackageRepository _package;
        private ProductDetailRepository _productDetail;
        private ProductImageRepository _productImage;
        private SuitableObjectRepository _suitableObject;
        private TypeRepository _type;
        public UnitOfWork()
        {
            _unitOfWorkContext ??= new NET1720_231_2_FENGSHUIKOIContext();
        }
        public UnitOfWork(NET1720_231_2_FENGSHUIKOIContext unitOfWorkContext)
        {
            _unitOfWorkContext ??= unitOfWorkContext;
        }
        public AccountRepository AccountRepository
        {
            get
            {
                return _account ??= new AccountRepository(_unitOfWorkContext);
            }
        }

        public ComboRepository ComboRepository
        {
            get
            {
                return _combo ??= new ComboRepository(_unitOfWorkContext);
            }
        }

        public ElementRepository ElementRepository
        {
            get
            {
                return _element ??= new ElementRepository(_unitOfWorkContext);
            }
        }

        public MemberPackageRepository MemberPackageRepository
        {
            get
            {
                return _memberPackage ??= new MemberPackageRepository(_unitOfWorkContext);
            }
        }
        public MemberRepository MemberRepository
        {
            get
            {
                return _member ??= new MemberRepository(_unitOfWorkContext);
            }
        }

        public PackageRepository PackageRepository
        {
            get
            {
                return _package ??= new PackageRepository(_unitOfWorkContext);
            }
        }        

        public ProductDetailRepository ProductDetailRepository
        {
            get
            {
                return _productDetail ??= new ProductDetailRepository(_unitOfWorkContext);
            }
        }        
        
        public ProductImageRepository ProductImageRepository
        {
            get
            {
                return _productImage ??= new ProductImageRepository(_unitOfWorkContext);
            }
        }        
        
        public SuitableObjectRepository SuitableObjectRepository
        {
            get
            {
                return _suitableObject ??= new SuitableObjectRepository(_unitOfWorkContext);
            }
        }        
        
        public TypeRepository TypeRepository
        {
            get
            {
                return _type ??= new TypeRepository(_unitOfWorkContext);
            }
        }
        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
        #endregion

    }
}
