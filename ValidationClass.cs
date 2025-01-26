#region
// Mikayle Coetzee
// ST10023767
// CLDV6212 POE 2023
// Part 1
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLDV6212_POE_ST10023767
{
    public class ValidationClass
    {
        /// <summary>
        /// Dictionary containing hard code vaccination data.
        /// </summary>
        private readonly Dictionary<string, string> VaccinationData = new Dictionary<string, string>
        {
            {"A12345689", "John Smith, Received Pfizer vaccine (PFZV230715) on 2023-07-15"},
            {"P97624679", "Alice Johnson, Received Malaria vaccine (MALV230801) on 2023-08-01"},
            {"C98765421", "Michael Williams, Received Johnson & Johnson vaccine (JNJV230801) on 2023-08-01"},
            {"A87365921", "Emily Brown, Received Johnson & Johnson vaccine (JNJV230802) on 2023-08-02"},
            {"A90326475", "David Jones, Received Pfizer vaccine (PFZV230803) on 2023-08-03"},
            {"C69182567", "Sophia Garcia, Received Malaria vaccine (MALV230804) on 2023-08-04"},
            {"P78531942", "Ella Martinez, Received Pfizer vaccine (PFZV230805) on 2023-08-05"},
            {"0306060076082", "Olivia Hernandez, Received Johnson & Johnson vaccine (JNJV230806) on 2023-08-06"},
            {"1001023456789", "Ava Jackson, Received Johnson & Johnson vaccine (JNJV230807) on 2023-08-07"},
            {"7107108270123", "Liam Anderson, Received Pfizer vaccine (PFZV230808) on 2023-08-08"},
            {"9903204678123", "Emma Wilson, Received Malaria vaccine (MALV230809) on 2023-08-09"},
            {"8912016345123", "Noah Taylor, Received Pfizer vaccine (PFZV230810) on 2023-08-10"},
            {"8006247895123", "William White, Received Johnson & Johnson vaccine (JNJV230811) on 2023-08-11"},
            {"7505278623123", "James Martin, Received Johnson & Johnson vaccine (JNJV230812) on 2023-08-12"},
        };

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor
        /// </summary>
        public ValidationClass()
        {

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will loop through all the data provided and return false if records is found with incorrect
        /// data, ask if it should update or dedlete it.
        /// </summary>
        public void ValidateTheDictionary()
        {
            foreach (var record in VaccinationData)
            {

            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Validates whether the input is a valid ID or passport number 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (bool isValid, string report) ValidateInput(string input)
        {
            if (IsValidId(input) || IsValidPassport(input))
            {
                var result = ProcessVaccinationData(input);
                return result;
            }
            else
            {
                return (false, "Invalid ID or Passport Number.");
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Validates whether the given string is a valid ID.
        /// </summary>
        /// <param name="id">The input string to validate.</param>
        /// <returns>True if the ID is valid; otherwise, false.</returns>
        private bool IsValidId(string id)
        {
            return id.Length == 13 && long.TryParse(id, out _);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Validates whether the given string is a valid passport number.
        /// </summary>
        /// <param name="passport">The input string to validate.</param>
        /// <returns>True if the passport number is valid; otherwise, false.</returns>
        private bool IsValidPassport(string passport)
        {
            return passport.Length == 9 &&
                   char.IsLetter(passport[0]) &&
                   passport.Substring(1).All(char.IsDigit);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Processes and displays vaccination data based on the provided key.
        /// </summary>
        /// <param name="key">The key (ID or passport number) to retrieve vaccination data.</param>
        private (bool isValid, string report) ProcessVaccinationData(string key)
        {
            string report;

            if (VaccinationData.TryGetValue(key, out var vaccinationInfo))
            {
                var lines = vaccinationInfo.Split(new[] { " on " }, StringSplitOptions.None);
                if (lines.Length == 2)
                {
                    var vaccineDetails = lines[0];
                    var date = lines[1];

                    var firstNameStartIndex = 0; // Start 
                    var firstNameEndIndex = vaccineDetails.IndexOf(" "); // End before the space

                    var name = vaccineDetails.Substring(firstNameStartIndex, firstNameEndIndex - firstNameStartIndex);

                    var surnameStartIndex = firstNameEndIndex + 1; // Start after the space
                    var surnameEndIndex = vaccineDetails.IndexOf(", Received"); // End before the comma

                    var surname = vaccineDetails.Substring(surnameStartIndex, surnameEndIndex - surnameStartIndex);

                    var vaccineCodeStartIndex = vaccineDetails.IndexOf("(") + 1; // Start after the opening parenthesis
                    var vaccineCodeEndIndex = vaccineDetails.IndexOf(")") - vaccineCodeStartIndex; // End before the closing parenthesis

                    var vaccineCode = vaccineDetails.Substring(vaccineCodeStartIndex, vaccineCodeEndIndex);

                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("----------------Vaccination Report----------------");
                    stringBuilder.AppendLine($"ID/Passport Number:\t{key}");
                    stringBuilder.AppendLine($"Name:\t\t\t{name}");
                    stringBuilder.AppendLine($"Surname:\t\t{surname}");
                    stringBuilder.AppendLine($"Vaccination Code:\t{vaccineCode}");
                    stringBuilder.AppendLine($"Vaccination Date:\t{date}");

                    if (IsValidId(key))
                    {
                        var dateOfBirth = GetDateOfBirthFromId(key);
                        var age = CalculateAge(dateOfBirth);
                        var gender = GetGenderFromId(key);
                        stringBuilder.AppendLine($"Date of Birth:\t\t{dateOfBirth:yyyy-MM-dd}");
                        stringBuilder.AppendLine($"Age:\t\t\t{age}");
                        stringBuilder.AppendLine($"Gender:\t\t\t{gender}");
                    }

                    stringBuilder.AppendLine("--------------------------------------------------");

                    return (true, stringBuilder.ToString());
                }
                else
                {
                    report = "Invalid vaccination information.";
                    return (false, report);
                }
            }
            else
            {
                report = "No vaccination information found.";
                return (false, report);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Retrieves date of birth from an ID.
        /// </summary>
        /// <param name="id">The ID from which to extract date of birth.</param>
        /// <returns>Date of birth.</returns>
        private DateTime GetDateOfBirthFromId(string id)
        {
            var yearPart = id.Substring(0, 2);
            var monthPart = id.Substring(2, 2);
            var dayPart = id.Substring(4, 2);
            var year = 0;

            if (DateTime.TryParseExact(yearPart, "yy", null,
                    System.Globalization.DateTimeStyles.None, out var parsedYear))
            {
                year = parsedYear.Year;
            }

            int.TryParse(monthPart, out var month);
            int.TryParse(dayPart, out var day);

            return new DateTime(year, month, day);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Calculates the age based on the provided birth date.
        /// </summary>
        /// <param name="birthDate">The birth date to calculate age from.</param>
        /// <returns>Age.</returns>
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Retrieves gender information from an ID.
        /// </summary>
        /// <param name="id">The ID from which to extract gender information.</param>
        /// <returns>Gender ("Male" or "Female").</returns>
        private string GetGenderFromId(string id)
        {
            var genderDigit = int.Parse(id.Substring(6, 4));
            return genderDigit < 5000 ? "Female" : "Male";
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
