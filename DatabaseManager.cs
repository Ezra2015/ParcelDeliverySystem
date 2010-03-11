using System;
using System.IO;
using System.Collections;

public class DatabaseManager
{
    private String dbpath;
    private ArrayList objects;
    public DatabaseManager(String dbpath)
    {
        this.dbpath = dbpath;
        this.objects = new ArrayList();


        if (File.Exists(this.dbpath))
        {
            // Console.WriteLine("File exists.");
            // loads all the text lines from the database text file
            // into array.
            StreamReader sr = new StreamReader(this.dbpath);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                this.objects.Add(line);
            }
            
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }

    public void AddObject(object dbobj) 
    {
        this.objects.Add(dbobj);
    }

    public void removeObject(int index)
    {
        this.objects.RemoveAt(index);
    }

    public void removeObject(object o)
    {
        this.objects.Remove(o);
    }

    public object GetObject(int index)
    {
        return this.objects[index];
    }

    public int Count
    {
        get
        {
            return this.objects.Count;
        }
    }

    
}