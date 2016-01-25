#pragma once
#include "Resource.h"
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

	void init(HINSTANCE hinst, rectangle win);
	void placeCar(Direction dir);
	void draw(HDC drawcanvas);
	void update();
	void checkOutofBounds();
	void setSpawnRate(int pw, int pn);
	void resize(rectangle newWind);
	void tick();
	void pwUP();
	void pwDOWN();
	void pnUP();
	void pnDOWN();

private:
	
	BitMap backGround;

	vector<Car*> northcars;
	vector<Car*> westcars;

	//percent chance of car spawn
	int pw;
	int pn; 

	vector<TrafficLight*> lights;
	HINSTANCE hinst;
	
	rectangle window;
	
	rectangle crossRect;

};

