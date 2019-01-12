library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity DataTrans is
	port(	clk		:in std_logic;
			Nx			:in integer range 50000000 downto 0;
			Ns			:in integer range 50000000 downto 0;
			SmgData	:out std_logic_vector(23 downto 0));
end DataTrans;

architecture behav of DataTrans is

	signal	b6:std_logic_vector(3 downto 0);
	signal	b5:std_logic_vector(3 downto 0);
	signal	b4:std_logic_vector(3 downto 0);
	signal 	b3:std_logic_vector(3 downto 0);
	signal	b2:std_logic_vector(3 downto 0);
	signal	b1:std_logic_vector(3 downto 0);
	signal 	da:std_logic_vector(3 downto 0);
	signal 	db:std_logic_vector(3 downto 0);
	signal 	dc:std_logic_vector(3 downto 0);
	signal 	dd:std_logic_vector(3 downto 0);
	signal 	de:std_logic_vector(3 downto 0);
	signal 	df:std_logic_vector(3 downto 0);
begin

	process(Nx,clk)
		variable num_in:integer range 999999 downto 0;
		variable temp:integer range 0 to 999999 := 0;	
	begin
		num_in := (( 50000000 / Ns ) * Nx ) rem 1000000;
		num_in := (num_in	* 5 / 4 ) +2;
		if(rising_edge(clk)) then
			if(temp < num_in) then
				if	(da = 9 and db = 9 and dc = 9 and dd = 9 and de =9 and df =9) then
					da <= "0000";	db <= "0000";	dc <= "0000";	dd <= "0000";	de <= "0000";	df <= "0000";
				elsif (da = 9 and db = 9 and dc = 9 and dd = 9 and de =9) then
					da <= "0000";	db <= "0000";	dc <= "0000";	dd <= "0000";	de <= "0000";	df <= df + 1;	temp := temp + 1;
				elsif (da = 9 and db = 9 and dc = 9 and dd = 9) then
					da <= "0000";	db <= "0000";	dc <= "0000";	dd <= "0000";	de <= de + 1;	temp := temp + 1;
				elsif (da = 9 and db = 9 and dc = 9) then
					da <= "0000";	db <= "0000";	dc <= "0000";	dd <= dd +1;	temp := temp + 1;
				elsif (da = 9 and db = 9) then
					da <= "0000"; 	db <= "0000";	dc <= dc + 1;	temp := temp + 1;
				elsif (da = 9) then
					da <= "0000"; 	db <= db + 1;	temp := temp + 1;
				else 
					da <= da + 1; temp := temp + 1;
				end if;
			else
				temp := 0;	da <= "0000";	db <= "0000";	dc <= "0000"; 	dd <= "0000";	de <= "0000";	df <= "0000";
				b1 <= da;	b2 <= db;	b3 <= dc;	b4 <= dd;	b5 <= de;	b6 <= df;
			end if;
		end if;
	end process;
	
	SmgData <= b6 & b5 & b4 & b3 & b2 & b1;
	
end behav;
