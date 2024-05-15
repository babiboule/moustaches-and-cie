using ScriptableObjects;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public enum PbFamily
    {
        TooOld,
        TooYoung,
        TooFar,
        NoCar,
        TooPoor,
        TooBusy,
        Child,
        NoOutdoor,
        OpenOutdoor,
        Outdoor,
        Animals,
        NoAnimals,
        Comment
    }
    
    public enum PbCat
    {
        Kitten,
        Sick,
        Disable,
        Shy,
        Lover,
        Outdoor,
        NoOkAnimals,
        NeedAnimals,
        None
    }
    
    public struct Problem
    {
        public bool Exists;
        public CatsScriptableObject.Cat Cat;
        public PbFamily PbFamily;
        public PbCat PbCat;
    }

    private static FamilyManager.Family _family;
    private static CatsScriptableObject.Cat _cat;


    public static Problem CheckProblem(FamilyManager.Family family, CatsScriptableObject.Cat cat)
    {
        _family = family;
        _cat = cat;
        
        Problem problem = new Problem();
        problem.Exists = false;
        problem.Cat = _cat;
        
        problem = CheckFamilyAge(problem);
        if (problem.Exists)
            return problem;
        
        problem = CheckFamilyHome(problem);
        if (problem.Exists)
            return problem;
        
        problem = CheckCatAge(problem);
        if (problem.Exists)
            return problem;

        problem = CheckSick(problem);
        if (problem.Exists)
            return problem;
        
        problem = CheckDisability(problem);
        if (problem.Exists)
            return problem;

        problem = CheckNature(problem);
        if (problem.Exists)
            return problem;

        problem = CheckOutdoor(problem);
        if (problem.Exists)
            return problem;

        problem = CheckAnimals(problem);
        if (problem.Exists)
            return problem;

        problem = CheckFamilyComment(problem);
        if (problem.Exists)
            return problem;

        
        return problem;
    }

    private static Problem CheckFamilyAge(Problem problem)
    {
        if (_family.Age < 18)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.TooYoung;
        }

        if (_family.Age > 70)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.TooOld;
        }

        return problem;
    }

    private static Problem CheckFamilyHome(Problem problem)
    {
        if (_family.Home.department != 67)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.TooFar;
        }
        /*
        else if (_family.Home.city != "Strasbourg")
        {
            if (!_family.Car)
            {
                problem.Exists = true;
                problem.PbCat = PbCat.None;
                problem.PbFamily = PbFamily.NoCar;
            }
        }
        */
        return problem;
    }

    private static Problem CheckCatAge(Problem problem)
    {
        if (_cat.age < 1)
        {
            problem.PbCat = PbCat.Kitten;
            
            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return problem;
            }
            if (_family.FreeTime <= 2 && !_family.Cats)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return problem;
            }
        }
        return problem;
    }  
    
    private static Problem CheckSick(Problem problem)
    {
        if (_cat.sick != CatsScriptableObject.Sick.Aucune)
        {
            problem.PbCat = PbCat.Sick;

            if (_family.Cats)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Animals;
                return problem;
            }
            if (_family.Outdoor == FamilyInfosScriptableObject.Outdoor.Ouvert)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.OpenOutdoor;
                return problem;
            }

            switch (_cat.sick)
            {
                case CatsScriptableObject.Sick.Coriza:
                    if (_family.Budget < 80)
                    {
                        problem.Exists = true;
                        problem.PbFamily = PbFamily.TooPoor;
                        return problem;
                    }
                    break;
                case CatsScriptableObject.Sick.FIV :
                    if (_family.Budget < 120)
                    {
                        problem.Exists = true;
                        problem.PbFamily = PbFamily.TooPoor;
                        return problem;
                    }
                    break;
                case CatsScriptableObject.Sick.PIF :
                    if (_family.Budget < 220)
                    {
                        problem.Exists = true;
                        problem.PbFamily = PbFamily.TooPoor;
                        return problem;
                    }
                    break;
            }
        }
        return problem;
    }

    private static Problem CheckDisability(Problem problem)
    {
        if (_cat.disability)
        {
            if (_family.Outdoor != FamilyInfosScriptableObject.Outdoor.Aucun)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Outdoor;
                problem.PbCat = PbCat.Disable;
                return problem;
            }
        }
        return problem;
    }
        
    private static Problem CheckNature(Problem problem)
    {
        if (_cat.nature == CatsScriptableObject.Nature.Collant)
        {
            problem.PbCat = PbCat.Lover;
            
            if (_family.FreeTime < 2 && !_family.Cats)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return problem;
            }
        }

        if (_cat.nature == CatsScriptableObject.Nature.Peureux)
        {
            problem.PbCat = PbCat.Shy;
            
            if (_family.Outdoor == FamilyInfosScriptableObject.Outdoor.Ouvert)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.OpenOutdoor;
                return problem;
            }
            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return problem;
            }
        }
        return problem;
    } 
        
    private static Problem CheckOutdoor(Problem problem)
    {
        if (_cat.outdoor)
        {
            problem.PbCat = PbCat.Outdoor;

            if (_family.Outdoor == FamilyInfosScriptableObject.Outdoor.Aucun)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.NoOutdoor;
                return problem;
            }
        }
        return problem;
    } 
    
        
    private static Problem CheckAnimals(Problem problem)
    {
        if (_family.Cats)
        {
            if (!_cat.animals)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Animals;
                problem.PbCat = PbCat.NoOkAnimals;
                return problem;
            }
        }

        return problem;
    } 
    
        
    private static Problem CheckFamilyComment(Problem problem)
    {
        if (_family.Comment.problematic)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.Comment;
            return problem;
        }
        return problem;
    } 
}
