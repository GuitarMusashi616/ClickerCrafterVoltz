﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscriber
{
	void UpdateFromPublisher(IPublisher publisher);
}
