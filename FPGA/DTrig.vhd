library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity DTrig is
	port(	cp		:in std_logic;
			D		:in std_logic;
			Q		:out std_logic;
			notQ	:out std_logic);
end DTrig;

architecture behav of DTrig is
begin
process(cp)
begin
	if(rising_edge(cp))then
		Q <= D;
		notQ <= not D;
	end if;
end process;
end behav;
