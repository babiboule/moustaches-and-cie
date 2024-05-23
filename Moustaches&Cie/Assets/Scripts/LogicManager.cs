using ScriptableObjects;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    // Why the family can't have the cat
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
    
    // Why the cat is not made for the family
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
    
    // Problem generated between the family and the cat 
    public struct Problem
    {
        public bool Exists;
        public CatsScriptableObject.Cat Cat;
        public PbFamily PbFamily;
        public PbCat PbCat;
    }

    // Private variables
    private static FamilyManager.Family _family;
    private static CatsScriptableObject.Cat _cat;


    /*
     * Check the problem between Param family and Param cat and return it
     */
    public static Problem CheckProblem(FamilyManager.Family family, CatsScriptableObject.Cat cat)
    {
        _family = family;
        _cat = cat;
        
        Problem problem = new()
        {
            Exists = false,
            Cat = _cat
        };

        // Check each logical connection between the family and the cat one by one and return the problem if there is one 
        
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
        
        return problem;
    }

    /*
     * Check the family age and return the updated Param problem
     */
    private static Problem CheckFamilyAge(Problem problem)
    {
        // Can't be a minor
        if (_family.Age < 18)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.TooYoung;
        }

        // Can't be over 70yo 
        if (_family.Age >= 70)
        {
            // If over 70yo and has a guarantor, it's ok
            if (StatsManager.instance.GetLevel() > 2 && _family.Guarantor)
                return problem;
            
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.TooOld;
        }

        return problem;
    }

    /*
     * Check the family location and return the updated Param problem
     */
    private static Problem CheckFamilyHome(Problem problem)
    {
        // Can't be out of Bas-Rhin
        if (_family.Home.department != 67)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.TooFar;
        }
        
        // If out of Strasbourg, must have a car
        else if (_family.Home.city != "Strasbourg" && StatsManager.instance.GetLevel() > 2)
        {
            if (!_family.Car)
            {
                problem.Exists = true;
                problem.PbCat = PbCat.None;
                problem.PbFamily = PbFamily.NoCar;
            }
        }
        
        return problem;
    }

    /*
     * Check the cat age and return the updated Param problem
     */
    private static Problem CheckCatAge(Problem problem)
    {
        // If the cat is a kitten (<1 year)
        if (_cat.age < 1)
        {
            problem.PbCat = PbCat.Kitten;
            
            // Can't have a child
            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return problem;
            }
            // Must have freetime or another cat
            if (_family is { FreeTime: < 2, Cats: false })
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return problem;
            }
        }
        return problem;
    }  
    
    /*
     * Check the cat sickness and return the updated Param problem
     */
    private static Problem CheckSick(Problem problem)
    {
        // If the cat is sick
        if (_cat.sick != CatsScriptableObject.Sick.Aucune)
        {
            problem.PbCat = PbCat.Sick;

            // Can't have another cat
            if (_family.Cats)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Animals;
                return problem;
            }
            // Can't have an open outdoor
            if (_family.Outdoor == FamilyInfosScriptableObject.Outdoor.Ouvert)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.OpenOutdoor;
                return problem;
            }

            // Different checks depending on the sickness
            switch (_cat.sick)
            {
                // Coriza : budget must be at least 80€/month
                case CatsScriptableObject.Sick.Coriza:
                    if (_family.Budget < 80)
                    {
                        problem.Exists = true;
                        problem.PbFamily = PbFamily.TooPoor;
                        return problem;
                    }
                    break;
                
                // FIV : budget must be at least 120€/month
                case CatsScriptableObject.Sick.FIV :
                    if (_family.Budget < 120)
                    {
                        problem.Exists = true;
                        problem.PbFamily = PbFamily.TooPoor;
                        return problem;
                    }
                    break;
                
                // PIF : budget must be at least 220€/month
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

    /*
     * Check the cat handicap and return the updated Param problem
     */
    private static Problem CheckDisability(Problem problem)
    {
        // If the cat is disabled
        if (_cat.disability)
        {
            // Can't have an outdoor
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
        
    /*
     * Check the cat nature and return the updated Param problem
     */
    private static Problem CheckNature(Problem problem)
    {
        // If the cat is a lover
        if (_cat.nature == CatsScriptableObject.Nature.Collant)
        {
            problem.PbCat = PbCat.Lover;
            
            // Must have free time or antoher cat
            if (_family is { FreeTime: < 2, Cats: false })
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return problem;
            }
        }

        // If the cat is shy
        if (_cat.nature == CatsScriptableObject.Nature.Peureux)
        {
            problem.PbCat = PbCat.Shy;
            
            // Can't have an open outdoor
            if (_family.Outdoor == FamilyInfosScriptableObject.Outdoor.Ouvert)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.OpenOutdoor;
                return problem;
            }
            
            // Can't have a child
            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return problem;
            }
        }
        return problem;
    } 
        
    /*
     * Check the family outdoor and return the updated Param problem
     */
    private static Problem CheckOutdoor(Problem problem)
    {
        // If the cat wants an outdoor
        if (_cat.outdoor)
        {
            problem.PbCat = PbCat.Outdoor;

            // Must have an outdoor
            if (_family.Outdoor == FamilyInfosScriptableObject.Outdoor.Aucun)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.NoOutdoor;
                return problem;
            }
        }
        return problem;
    } 
    
    /*
     * Check the family other cats and return the updated Param problem
     */
    private static Problem CheckAnimals(Problem problem)
    {
        // If family has other cats
        if (_family.Cats)
        {
            // The cat must be ok with it
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
    
    /*
     * Check the family comment and return the updated Param problem
     */
    private static Problem CheckFamilyComment(Problem problem)
    {
        // If the comment is problematic, they can't have any cat lol
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