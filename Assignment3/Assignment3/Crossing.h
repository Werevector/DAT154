#pragma once
#include <vector>
#include "Car.h"
#include "TrafficLight.h"
#include "Structs.h"
using namespace std;

class Crossing
{
public:
	Crossing();
	~Crossing();

	void init(rectangle win);
	void draw(HDC drawcanvas);
	void update();
	void tick();

private:
	
	vector<Car*> cars;
	vector<TrafficLight*> lights;
	
	rectangle window;
	
	rectangle crossRect;

};

