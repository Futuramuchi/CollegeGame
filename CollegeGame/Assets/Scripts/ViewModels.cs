using System;

[System.Serializable]
public class Student
{
    public int Id;
    public string FirstName;
    public string MiddleName;
    public string LastName;
    public string Login;
    public string Password;
    public int GroupId;
    public string GroupName;
    public string CouratorName;
    public int CharacterId;
}

[System.Serializable]
public class Journal
{
    public int Id;
    public int Mark;
    public int TeacherId;
    public string TeacherName;
    public int StudentId;
    public string StudentName;
    public DateTime Date;
    public int DesciplineId;
    public string DeciplineName;
}