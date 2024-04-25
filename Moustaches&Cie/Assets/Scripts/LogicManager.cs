using ScriptableObjects;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public enum PbFamily
    {
        TooOld,
        TooYoung,
        TooPoor,
        TooBusy,
        Child,
        NoOutdoor,
        Animals,
        Comment
    }
    
    public enum PbCat
    {
        Kitten,
        Sick,
        Shy,
        Agressive,
        Outdoor,
        Animals,
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
        
        problem = CheckCatAge(problem);
        if (problem.Exists)
            return problem;

        problem = CheckSick(problem);
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
            if (_family.Age > 90)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooOld;
                return problem;
            }
            if (_family.FreeTime < 2)
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
        if (_cat.sick)
        {
            problem.PbCat = PbCat.Sick;

            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return problem;
            }
            if (_family.Income < 1000)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooPoor;
                return problem;
            }
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return problem;
            }
        }
        return problem;
    } 
    
        
    private static Problem CheckNature(Problem problem)
    {
        if (_cat.nature == CatsScriptableObject.Nature.Peureux)
        {
            problem.PbCat = PbCat.Shy;
            
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return problem;
            }
        }

        if (_cat.nature == CatsScriptableObject.Nature.Agressif)
        {
            problem.PbCat = PbCat.Agressive;
            
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
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

            if (!_family.Outdoor)
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
        if (!_cat.animals)
        {
            problem.PbCat = PbCat.Animals;

            if (_family.Animals)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Animals;
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
