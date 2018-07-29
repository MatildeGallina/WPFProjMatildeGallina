using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public abstract class BaseValidator : IValidator
    {
        public List<string> Validate(NodeModel model)
        {
            var errors = new List<string>();
            
                if (IsNotValid(model))
                    errors.Add(ErrorMessage());

            return errors;
        }

        internal abstract bool IsNotValid(NodeModel model);
        internal abstract string ErrorMessage();
    }


    public class NameValidator : BaseValidator
    {
        internal override bool IsNotValid(NodeModel model)
        {
            return string.IsNullOrWhiteSpace(model.Name);
        }

        internal override string ErrorMessage()
        {
            return "Name not valid!";
        }
    }


    public class SerialCodeValidator : BaseValidator
    {
        internal override bool IsNotValid(NodeModel model)
        {
            return string.IsNullOrWhiteSpace(model.SerialCode) || model.SerialCode.Length != 10;
        }

        internal override string ErrorMessage()
        {
            return "SerialCode not valid!";
        }
    }

    public class Validator
    {
        public Validator()
        {
            _validators = CreateList();
        }

        private List<BaseValidator> _validators { get; }

        private List<BaseValidator> CreateList()
        {
            var validators = new List<BaseValidator>()
            {
                new NameValidator(),
                new SerialCodeValidator(),
            };

            return validators;
        }

        public List<string> ValidateNode(NodeModel model)
        {
            var totalErrors = new List<string>();

            foreach(var v in _validators)
            {
                var errors = v.Validate(model);
                foreach (var e in errors)
                    totalErrors.Add(e);
            }

            return totalErrors;
        }
    }
}
