using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<ISubject> subjects;
        private IRepository<IStudent> students;
        private IRepository<IUniversity> universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject)
                && subjectType != nameof(HumanitySubject)
                && subjectType != nameof(TechnicalSubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            ISubject subject = subjects.FindByName(subjectName);
            if (subject != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            int currentSubjectId = subjects.Models.Count + 1;

            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(currentSubjectId, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(currentSubjectId, subjectName);
            }
            else if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(currentSubjectId, subjectName);
            }
            subjects.AddModel(subject);

            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            IUniversity university = universities.FindByName(universityName);

            if (university != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            List<int> requiredSubjectsId = new List<int>();

            foreach (var subject in requiredSubjects)
            {
                ISubject currentSubject = subjects.FindByName(subject);

                requiredSubjectsId.Add(currentSubject.Id);
            }

            int currentUniversityId = universities.Models.Count + 1;

            IUniversity universityToMake = new University(
                currentUniversityId,
                universityName,
                category, 
                capacity,
                requiredSubjectsId);

            universities.AddModel(universityToMake);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            int currentStudentId = students.Models.Count + 1;
            IStudent student = new Student(currentStudentId, firstName, lastName);

            students.AddModel(student);

            return string.Format(OutputMessages.StudentAddedSuccessfully,firstName, lastName, students.GetType().Name);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);

            if(student == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            ISubject subject = subjects.FindById(subjectId);

            if (subject == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            if (student.CoveredExams.Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, 
                    student.FirstName, 
                    student.LastName, 
                    subject.Name);
            }

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, 
                    student.FirstName,
                    student.LastName,
                    subject.Name);
        }


        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] fullName = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IStudent student = students.FindByName(studentName);

            if(student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, fullName[0], fullName[1]);
            }

            IUniversity university = universities.FindByName(universityName);

            if(university == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }


            foreach (var requiredSubject in university.RequiredSubjects)
            {
                ISubject subject = subjects.FindById(requiredSubject);

                if (!student.CoveredExams.Contains(subject.Id))
                {
                    return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }
            }

            if(student.University != null &&  student.University == universities.FindByName(universityName))
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, fullName[0], fullName[1], universityName);
            }

            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, fullName[0], fullName[1], universityName);
        }


        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            StringBuilder sb = new();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");

            int studentsAdmitted = students.Models.Where(x => x.University == university).Count();

            sb.AppendLine($"Students admitted: {studentsAdmitted}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentsAdmitted}");

            return sb.ToString().TrimEnd();
        }
    }
}
