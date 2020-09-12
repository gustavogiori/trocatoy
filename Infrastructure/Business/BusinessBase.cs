using Infrastructure.Filter;
using Infrastructure.Models;
using Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Business
{
    public abstract class BusinessBase<T> : IBusinessBase<T> where T : EntityBase
    {
        /// <summary>
        /// _repository
        /// </summary>
        protected readonly IRepository<T> _repository;
        /// <summary>
        /// construtor
        /// </summary>
        /// <param name="repository"></param>
        public BusinessBase(IRepository<T> repository)
        {
            this._repository = repository;
        }
        public virtual void GeraNovoGuid(T obj)
        {
            if (GConvert.IsGuidEmpty(obj.Id))
            {
                obj.Id = Guid.NewGuid();
            }
        }
        public virtual void Delete(Guid id)
        {
            this._repository.Delete(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IEnumerable<T> GetAll(PaginationFilter filter, out int countPages)
        {
            return _repository.GetAll(filter, out countPages);
        }

        public virtual IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate, PaginationFilter filter, out int countPages)
        {
            return _repository.GetByCriteria(predicate, filter, out countPages);
        }

        public virtual IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetByCriteria(predicate);
        }

        public virtual T GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public virtual Tuple<T, ValidationModel> Insert(T obj)
        {
            var statusModel = IsValid(obj);

            if (statusModel.IsValid)
            {
                obj = _repository.Insert(obj);
            }
            return new Tuple<T, ValidationModel>(obj, statusModel);
        }

        public virtual ValidationModel IsValid(T obj)
        {
            ValidationContext vc = new ValidationContext(obj);
            ICollection<ValidationResult> results = new List<ValidationResult>(); // Will contain the results of the validation
            bool isValid = Validator.TryValidateObject(obj, vc, results, true);
            return GetValidationModel(results, isValid);
        }

        private ValidationModel GetValidationModel(ICollection<ValidationResult> validationResults, bool IsValid)
        {
            ValidationModel validationModel = new ValidationModel();
            validationModel.IsValid = IsValid;
            validationModel.ErrorMessage = new List<string>();

            foreach (var result in validationResults)
            {
                validationModel.ErrorMessage.Add(result.ErrorMessage);
            }
            return validationModel;
        }
        public virtual Tuple<T, ValidationModel> Update(T obj)
        {
            var statusModel = IsValid(obj);

            if (statusModel.IsValid)
            {
                obj = _repository.Update(obj);
            }

            return new Tuple<T, ValidationModel>(obj, statusModel);
        }
    }
}
