#include "stdafx.h"
#include "Car.h"


Car::Car(int x, int y, Direction dir, lightState *lightptr)
{
	position.x = x;
	position.y = y;

	velocity.x = 0;
	velocity.y = 0;

	maxSpeed.x = 3;
	maxSpeed.y = 2;

	acceleration = 0;
	maxAcceleration = 5;

	direction = dir;

	switch (dir) {
	case WEST:
		boundingBox.w = 100;
		boundingBox.h = 50;
		break;
	case NORTH:
		boundingBox.w = 50;
		boundingBox.h = 100;
	}

	boundingBox.x = x;
	boundingBox.y = y;

	this->lightptr = lightptr;

}


Car::~Car()
{
	DeleteObject(bitMap.image);
}

void Car::draw(HDC drawcanvas)
{

	int nx = boundingBox.x;
	int ny = boundingBox.y;

	HDC carDC = CreateCompatibleDC(drawcanvas);
	//HDC stretched = CreateCompatibleDC(drawcanvas);

	SelectObject(carDC, bitMap.image);
	
	SetStretchBltMode(carDC, COLORONCOLOR);
	//StretchBlt(stretched, nx, ny, boundingBox.w, boundingBox.h, carDC, 0,0,bitMap.width,bitMap.height, SRCCOPY);
	
	bool d = TransparentBlt(
		drawcanvas, 
		nx, ny, 
		boundingBox.w, boundingBox.h, 
		carDC,
		0, 0, 
		boundingBox.w, boundingBox.h,
		RGB(128, 128, 128));

	//Rectangle(drawcanvas, position.x, position.y, position.x+10, position.y+10);

	DeleteObject(carDC);
	//DeleteObject(stretched);

}

void Car::update(rectangle crossing)
{
	updateState(crossing);
	updateMovement();

}

rectangle* Car::getBounding() {
	return &boundingBox;
}

void Car::setNextCar(rectangle* next) {
	nextCarBounding = next;
}

void Car::freeNextCar() {
	nextCarBounding = nullptr;
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
			acceleration = breakAcc;
		}
		else {
			acceleration = 0;
			velocity.x = 0;
			velocity.y = 0;
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

	boundingBox.x = position.x;
	boundingBox.y = position.y;
}

bool Car::outsideRect(rectangle rect) {
	bool result = false;
	
	if (position.x > rect.x + rect.w || 
		position.y > rect.y + rect.h
		//position.x < rect.x ||
		//position.y < rect.y
		)
		result = true;
	return result;
}

void Car::updateState(rectangle crossing)
{
	float vel;
	float carpos;
	float crosspoint;

	if (nextCarBounding != nullptr) {
		if (nextCarBounding->x < crossing.x || nextCarBounding->y < crossing.y)
			crossing = *nextCarBounding;
	}
	
	switch (direction) {
	case WEST:
		vel = velocity.x;
		crosspoint = crossing.x;
		carpos = position.x + boundingBox.w;
		break;
	case NORTH:
		vel = velocity.y;
		crosspoint = crossing.y;
		carpos = position.y + boundingBox.h;
		break;
	}

	if (carpos > crosspoint-15 && carpos < crosspoint+5) {
		if (lightptr->green) {
			driving = true;
		}
		else {
			driving = false;
		}
	}
	else {
		driving = true;
	}
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

