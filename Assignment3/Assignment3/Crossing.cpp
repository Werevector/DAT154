#include "stdafx.h"
#include "Crossing.h"


Crossing::Crossing()
{
	TrfLight l;
	for (int i = Direction::NORTH; i < Direction::WEST; i++) {
		trafficLights.push_back(l);
	}
}


Crossing::~Crossing()
{
}

void Crossing::draw(HDC drawcanvas)
{
	vector<Car*>::iterator carIter;
	for (carIter = cars.begin(); carIter != cars.end(); carIter++ ) {
		(*carIter)->draw(drawcanvas);
	}
}

void Crossing::update()
{
}
