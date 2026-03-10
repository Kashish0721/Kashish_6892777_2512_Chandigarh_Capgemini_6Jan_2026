using System;

class Patient
{
    public string _name;
    public int _age;
    public string _illness;
    public string _city;

    public Patient()
    {

    }
    public Patient(string Name, int Age, string Illness, string City)
    {
        this._name = Name;
        this._age = Age;
        this._illness = Illness;
        this._city = City;
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }
    public string Illness
    {
        get { return _illness; }
        set { _illness = value; }
    }
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    public override string ToString()
    {
        return string.Format("{0,-21}{1,-6}{2,-17}{3,-20}",
                             this._name, this._age, this._illness, this._city);
    }

}