using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice.Interpretations
{
    public abstract class Interpretation<T> : IInterpretation
    {
        public Interpretation(IEnumerable<T> permittedRepresentations)
        {
            if (permittedRepresentations == null)
            {
                throw new ArgumentNullException(nameof(permittedRepresentations));
            }
            else if (!permittedRepresentations.Any())
            {
                throw new ArgumentException($"{nameof(permittedRepresentations)} cannot be empty.");
            }

            this.PermittedRepresentations = permittedRepresentations;
        }

        public virtual T PrimaryRepresentation
        {
            get
            {
                return this.PermittedRepresentations.First();
            }
            protected set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Primary representation cannot be null.");
                }
                else
                {
                    this.PermittedRepresentations = new T[] { value }.Concat(this.PermittedRepresentations);
                }
            }
        }

        public virtual IEnumerable<T> PermittedRepresentations { get; protected set; }

        public abstract bool Compare(IInterpretation other);

        public virtual bool CompareAll(IEnumerable<IInterpretation> list)
        {
            return list.All(item => this.Compare(item));
        }

        public virtual bool CompareAny(IEnumerable<IInterpretation> list)
        {
            return list.Any(item => this.Compare(item));
        }
    }
}
