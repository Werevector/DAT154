#include "stdafx.h"
#include "TrafficLight.h"


TrafficLight::TrafficLight(int x, int y)
{
	lightrect.x = x;
	lightrect.y = y;
	lightrect.w = 30;
	lightrect.h = 60;

	lights.green = true;
	lights.yellow = false;
	lights.red = false;

}


TrafficLight::~TrafficLight()
{
}

void TrafficLight::draw(HDC drawcanvas)
{
	HBRUSH rbrush = CreateSolidBrush(RGB(255,0,0));
	HBRUSH gbrush = CreateSolidBrush(RGB(0, 255, 0));
	HBRUSH ybrush = CreateSolidBrush(RGB(255, 255, 0));
	HBRUSH bbrush = CreateSolidBrush(RGB(0, 0, 0));
	HBRUSH grbrush = CreateSolidBrush(RGB(100, 100, 100));
	
	SelectObject(drawcanvas, bbrush);
	Rectangle(drawcanvas,
		lightrect.x,
		lightrect.y,
		lightrect.x + lightrect.w,
		lightrect.y + lightrect.h
		);

	//RED
	if(red)
		SelectObject(drawcanvas, rbrush);
	else
		SelectObject(drawcanvas, grbrush);
	Ellipse(drawcanvas, lightrect.x+5, lightrect.y+3, lightrect.x + 25, lightrect.y + 18 );
	
	//YELLOW
	if(yellow)
		SelectObject(drawcanvas, ybrush);
	else
		SelectObject(drawcanvas, grbrush);
	Ellipse(drawcanvas, lightrect.x+5, lightrect.y + 21, lightrect.x + 25, lightrect.y + 36);

	//GREEN
	if(green)
		SelectObject(drawcanvas, gbrush);
	else
		SelectObject(drawcanvas, grbrush);
	Ellipse(drawcanvas, lightrect.x+5, lightrect.y + 39, lightrect.x + 25, lightrect.y + 54);

	
	DeleteObject(rbrush);
	DeleteObject(gbrush);
	DeleteObject(ybrush);
	DeleteObject(bbrush);
	DeleteObject(grbrush);
}

void TrafficLight::update()
{

}

void TrafficLight::tick()
{
	
	if (red) {
		if (yellow) {
			red = false;
			yellow = false;
			green = true;
		}
		else
			yellow = true;
	}
	else if (yellow) {
		yellow = false;
		red = true;
	}
	else {
		yellow = true;
		green = false;
	}

	lights.green = green;
	lights.yellow = yellow;
	lights.red = red;
	
}

lightState* TrafficLight::getStatePtr() {
	return &lights;
}

void TrafficLight::setPos(float x, float y)
{
	lightrect.x = x;
	lightrect.y = y;
}

void TrafficLight::setStop() {
	green = false;
	yellow = false;
	red = true;

	lights.green = false;
	lights.yellow = false;
	lights.red = true;
}

void TrafficLight::setGo() {
	green = true;
	yellow = false;
	red = false;

	lights.green = true;
	lights.yellow = false;
	lights.red = false;
}