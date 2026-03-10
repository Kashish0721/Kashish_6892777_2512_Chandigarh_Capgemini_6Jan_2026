using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Employee
{
    private string _name;   
    private int _age;
    private string _designation;
    private string _city;

    public Employee()
        { }
    public Employee(string Name, int Age, string Designation, string City)
    {
        this._name = Name;
        this._age = Age;
        this._designation = Designation;
        this._city = City;
    }
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            _age = value;
        }
    }
    public string Designation
    {
        get
        {
            return _designation;
        }
        set
        {
            _designation = value;
        }
    }
    public string City
    {
        get
        {
            return _city;
        }
        set
        {
            _city = value;
        }
    }

    public override string ToString()
    {
        return string.Format("{0,-21}{1,-6}{2,-21}{3,-20}", this._name, this._age, this._designation, this._city);
    }
}

