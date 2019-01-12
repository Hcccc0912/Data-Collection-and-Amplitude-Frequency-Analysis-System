library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity SmgWeiCode is
	port(clk:in std_logic;
		rst_n:in std_logic;
		Wei:out std_logic_vector(5 downto 0));
end SmgWeiCode;

architecture behav of SmgWeiCode is
constant T1MS:integer:= 4_9999;
signal count:integer range 4_9999 downto 0;
signal SelectWei:integer range 5 downto 0;
begin
process(clk,rst_n)
begin
	if(rst_n = '0')then
		count <= 0;
	elsif(rising_edge(clk))then
		if(count = T1MS)then
			count <= 0;
		else
			count <= count + 1;
		end if;
	end if;
end process;
process(clk,rst_n)
begin
	if(rst_n = '0')then
		SelectWei <= 0;
		Wei <= b"111_111";
	elsif(rising_edge(clk))then
		case SelectWei is
			when 0 => if(count = T1MS)then SelectWei <= SelectWei + 1;else Wei <= b"011_111";end if;
			when 1 => if(count = T1MS)then SelectWei <= SelectWei + 1;else Wei <= b"101_111";end if;
			when 2 => if(count = T1MS)then SelectWei <= SelectWei + 1;else Wei <= b"110_111";end if;
			when 3 => if(count = T1MS)then SelectWei <= SelectWei + 1;else Wei <= b"111_011";end if;
			when 4 => if(count = T1MS)then SelectWei <= SelectWei + 1;else Wei <= b"111_101";end if;
			when 5 => if(count = T1MS)then SelectWei <= 0;				 else Wei <= b"111_110";end if;
			when others => Wei <= b"111_111";
		end case;
	end if;
end process;
end behav;