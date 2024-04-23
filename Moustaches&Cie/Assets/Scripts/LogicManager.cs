using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public enum PbFamily
    {
        TooOld,
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
        Problem problem = new Problem();
        problem.Exists = false;
        problem.Cat = _cat;

        if (CheckAge(problem))
            return problem;
        if (CheckSick(problem))
            return problem;
        if (CheckNature(problem))
            return problem;
        if (CheckOutdoor(problem))
            return problem;
        if (CheckAnimals(problem))
            return problem;
        if (CheckFamilyComment(problem))
            return problem;
        
        return problem;
    }

    private static bool CheckAge(Problem problem)
    {
        if (_cat.age < 1)
        {
            problem.PbCat = PbCat.Kitten;
            
            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return true;
            }
            if (_family.Age > 90)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooOld;
                return true;
            }
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return true;
            }
        }
        return false;
    }  
    
    private static bool CheckSick(Problem problem)
    {
        if (_cat.sick == true)
        {
            problem.PbCat = PbCat.Sick;

            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return true;
            }
            if (_family.Income < 1500)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooPoor;
                return true;
            }
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return true;
            }
        }
        return false;
    } 
    
        
    private static bool CheckNature(Problem problem)
    {
        if (_cat.nature == CatsScriptableObject.Nature.Peureux)
        {
            problem.PbCat = PbCat.Shy;
            
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return true;
            }
        }

        if (_cat.nature == CatsScriptableObject.Nature.Agressif)
        {
            problem.PbCat = PbCat.Agressive;
            
            if (_family.FreeTime < 2)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.TooBusy;
                return true;
            }
            if (_family.Child)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Child;
                return true;
            }
        }
        return false;
    } 
    
        
    private static bool CheckOutdoor(Problem problem)
    {
        if (_cat.outdoor)
        {
            problem.PbCat = PbCat.Outdoor;

            if (!_family.Outdoor)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.NoOutdoor;
                return true;
            }
        }
        return false;
    } 
    
        
    private static bool CheckAnimals(Problem problem)
    {
        if (!_cat.animals)
        {
            problem.PbCat = PbCat.Animals;

            if (_family.Animals)
            {
                problem.Exists = true;
                problem.PbFamily = PbFamily.Animals;
                return true;
            }
        }
        return false;
    } 
    
        
    private static bool CheckFamilyComment(Problem problem)
    {
        if (_family.Comment.problematic)
        {
            problem.Exists = true;
            problem.PbCat = PbCat.None;
            problem.PbFamily = PbFamily.Comment;
            return true;
        }
        return false;
    } 
}
