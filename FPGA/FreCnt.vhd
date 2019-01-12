library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity FreCnt is
	port(	en		:in std_logic;
			clk	:in std_logic;
			reset	:in std_logic;
			Nx		:out integer range 50000000 downto 0);
end FreCnt;

architecture behav of FreCnt is

signal count:integer range 50000000 downto 0;

begin

process(clk)
begin
	if(rising_edge(clk))then
		if(reset = '0')then
			count <= 0;
		elsif(en = '1')then
			count <= count + 1;
		else
			count <= 0;
		end if;
	end if;
end process;

Nx <= count;
end behav;