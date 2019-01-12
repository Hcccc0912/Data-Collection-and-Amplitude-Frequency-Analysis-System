library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity GateSigGen is
	port(	clk	:in std_logic;
			reset	:in std_logic;
			tp		:out std_logic);
end GateSigGen;

architecture behav of GateSigGen is

constant num:integer:=40_000_000;
signal count:integer range 50000000 downto 0:=0;
signal tpbuffer:std_logic:='0';

begin

process(clk)
	begin
		if(reset = '0')then
			tpbuffer <= '0';
			count <= 0;
		elsif(rising_edge(clk))then
			if(count = num)then
				count <= 0;
				tpbuffer <= not tpbuffer;
			else
				count <= count + 1;
			end if;
		end if;
	end process;
tp <= tpbuffer;
end behav;
