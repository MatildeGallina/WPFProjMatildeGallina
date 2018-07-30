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
        public SerialCodeValidator(IDatabase database)
        {
            _database = database;
        }

        private IDatabase _database { get; set; }

        internal override bool IsNotValid(NodeModel model)
        {
            return string.IsNullOrWhiteSpace(model.SerialCode) ||
                model.SerialCode.Length != 10 ||
                UniqueValue(model);
        }

        private bool UniqueValue(NodeModel model)
        {
            var serialCodes = _database.GetSerialCodes();

            foreach(var s in serialCodes)
                if (model.SerialCode == s)
                    return true;

            return false;
        }

        internal override string ErrorMessage()
        {
            return "SerialCode not valid!";
        }
    }

    public class Validator
    {
        public Validator(IDatabase database)
        {
            _database = database;
            _validators = CreateList();
        }
        private IDatabase _database { get; set; }
        private List<BaseValidator> _validators { get; }

        private List<BaseValidator> CreateList()
        {
            var validators = new List<BaseValidator>()
            {
                new NameValidator(),
                new SerialCodeValidator(_database),
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
