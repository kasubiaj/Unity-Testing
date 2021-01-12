using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOpenenable
{
    bool Opened { get; set; }

    void Open();
    void Close();
}
