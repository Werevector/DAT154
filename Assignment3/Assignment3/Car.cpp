#include "stdafx.h"
#include "Car.h"


Car::Car(int x, int y, Direction dir, TrafficLight *lightptr)
{
	position.x = x;
	position.y = y;

	velocity.x = 0;
	velocity.y = 0;

	maxSpeed.x = 1;
	maxSpeed.y = 1;

	acceleration = 0;
	maxAcceleration = 2;

	direction = dir;

	this->lightptr = lightptr;

}


Car::~Car()
{
	DeleteObject(bitMap.image);
}

void Car::draw(HDC drawcanvas)
{
	int nx = position.x - (bitMap.width / 2);
	int ny = position.y - (bitMap.height / 2);

	HDC carDC = CreateCompatibleDC(drawcanvas);

	SelectObject(carDC, bitMap.image);
	
	SetStretchBltMode(carDC, BLACKONWHITE);
	StretchBlt(drawcanvas, nx, ny, 100, 50, carDC, 0,0,600,200, SRCCOPY);

	wstring sp = _T("speedX: " + to_wstring(velocity.x));
	wstring a = _T("acceleration: " + to_wstring(acceleration));

	TextOut(drawcanvas, 10, 10, sp.c_str(), sp.size());
	TextOut(drawcanvas, 10, 30, a.c_str(), a.size());

	DeleteObject(carDC);

}

void Car::update()
{
	updateMovement();

}


void Car::updateMovement()
{
	if (driving) {
		if (velocity.x < maxSpeed.x && velocity.y < maxSpeed.y) {
			if (acceleration < maxAcceleration) {
				acceleration += 0.1;
			}

		}
		else {
			acceleration = 0;
		}
	}
	else {
		if (velocity.x > 0 && velocity.y > 0) {
			acceleration -= 0.3;
		}
		else {
			acceleration = 0;
		}
	}

	switch (direction) {
	case WEST:
		velocity.x += acceleration / 1000;
		break;
	case NORTH:
		velocity.y += acceleration / 1000;
		break;
	}

	position.x += velocity.x;
	position.y += velocity.y;
}

void Car::updateState()
{
}

void Car::drive()
{
	driving = true;
}

void Car::halt()
{
	driving = false;
}

bool Car::loadBitMap(HINSTANCE hInst, int IDB, int w, int h)
{
	bool result = true;
	bitMap.image = LoadBitmap(hInst, MAKEINTRESOURCE(IDB));
	bitMap.width = w;
	bitMap.height = h;
	return result;
}

