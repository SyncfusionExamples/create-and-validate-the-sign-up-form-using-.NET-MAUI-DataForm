namespace SignUpForm
{
    /// <summary>
    /// Represents the view model class for data form.
    /// </summary>
    public class SignUpFormViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpFormViewModel" /> class.
        /// </summary>
        public SignUpFormViewModel()
        {
            this.SignUpFormModel = new SignUpFormModel();

        }

        /// <summary>
        /// Gets or sets the Sign up model.
        /// </summary>
        public SignUpFormModel SignUpFormModel { get; set; }

    }
}
