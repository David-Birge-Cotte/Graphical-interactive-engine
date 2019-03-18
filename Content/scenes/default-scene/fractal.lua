-- Draws a fractal in background when added to an entity

max_it = 15;
pix_size = 256;
scale = 2;

-- _initialize is called by the engine
function _initialize ()
    -- adds the sprite component where the fractal will be drawn
    _entity.SetPosition(_entity.GetPosition().x, _entity.GetPosition().y);
    -- change entity size
    _entity.Scale(scale, scale);
    -- adds the sprite component where the fractal will be drawn
    spr = _entity.addSpriteComponent(); 
    -- put in background
    spr.SetSortingOrder(1); 
    -- call a Lua function
    draw_fractal();
end

function draw_fractal()
    -- create a table of color values
    local pixels = fill_fractal(); -- can take a lot of time
    -- use the new pixels table as the texture for the sprite
    spr.ChangeTextureData(pixels, pix_size, pix_size);
    -- change the color of the sprite using _color library
    spr.ChangeColor(_color.RandomGaussianColor());
end

-- create a table of pixels containing greyscale value from a computed fractal
function fill_fractal()
    -- the table containing pixels values
    local pixels = {};
    -- x position, y position and total iterations
    local x_, y_, i_ = 1, 1, 1;

    while (x_ <= pix_size) do
        while (y_ <= pix_size) do
            -- calculate fractal value for this x y position
            local x_pos = map(x_, 1, pix_size, -2, 1);
            local y_pos = map(y_, 1, pix_size, -1.5, 1.5);
            local c = mandelbrot(x_pos, y_pos);
            -- convert it to a 0-255 greyscale representation
            local val = c / max_it * 255;
            -- insert the new color into the table of pixels
            pixels[i_] = _color.NewColor(val, val, val, 255);
            -- increment
            y_ = y_ + 1;
            i_ = i_ + 1;
        end
        y_ = 1;
        x_ = x_ + 1;
    end
    return pixels;
end

-- classic implementation of mandelbrot fractal
function mandelbrot(x, y)
    local i, pt, sqr = 0, {x = x, y = y}, {};

    sqr.x = pt.x * pt.x;
    sqr.y = pt.y * pt.y;
	while (i < max_it and sqr.x + sqr.y < 4) do
		pt.y = pt.y * pt.x;
		pt.y = pt.y + pt.y;
		pt.y = pt.y + y;
		pt.x = sqr.x - sqr.y + x;
		sqr.x = (pt.x * pt.x);
		sqr.y = (pt.y * pt.y);
		i = i + 1;
    end
	if (i == max_it) then i = 0 end; -- inside of fractal is black
	return (i);
end

-- maps a value from a range to another
function map(input, input_s, input_e, output_s, output_e)
	local slope = (output_e - output_s) / (input_e - input_s);
    local output = output_s + slope * (input - input_s);
    
	return (output);
end