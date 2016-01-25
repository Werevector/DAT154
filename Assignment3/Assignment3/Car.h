#pragma once
#include <vector>
#include <string>
#include "TrafficLight.h"
#include "Structs.h"

using namespace std;
class Car
{
public:
	Car(int x, int y, Direction dir, lightState *lightptr);
	~Car();

	void draw(HDC drawcanvas);
	void update(rectangle crossing);
	
	rectangle* getBounding();
	
	void setNextCar(rectangle * next);
	void freeNextCar();
	
	void drive();
	void halt();

	bool outsideRect(rectangle rect);

	bool loadBitMap(HINSTANCE hInst, int IDB, int w, int h);


private:
	
	bool driving = true;
	Point position;
	Vector2d velocity;
	float acceleration;
	BitMap bitMap;
	Direction direction;

	void updateMovement();
	
	void updateState(rectangle crossing);

	Vector2d maxSpeed;
	float maxAcceleration;
	float breakAcc = -20;

	rectangle boundingBox;
	rectangle* nextCarBounding;

	lightState *lightptr = nullptr;

};

