public float MyFWidth(float v){
    return abs(ddx(v) * 2) + abs(ddy(v) * 2);
}