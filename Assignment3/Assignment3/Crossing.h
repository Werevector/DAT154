#pragma once
#include <vector>
#include "Car.h"
using namespace std;

struct TrfLight {
	bool green = true;
	bool yellow = false;
	bool red = false;
};

class Crossing
{
public:
	Crossing();
	~Crossing();

	void draw(HDC drawcanvas);
	void update();

private:
	
	vector<Car*> cars;
	vector<TrfLight> trafficLights;
	

};

