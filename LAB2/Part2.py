def spc(string):
    print(string * 60)

def devide(dividend, divisor):
    quotient = 0
    shift = dividend.bit_length() - divisor.bit_length()
    while shift >= 0:
        quotient = quotient << 1
        print("  {:20b}".format(dividend), "  {:20b}".format(quotient))
        if dividend >= (divisor << shift):
            print("- {:20b}".format((divisor << shift)))
            dividend -= divisor << shift
            quotient += 1
        spc("_")
        shift -= 1
    print("  {:20b}".format(dividend))
    return quotient

def devide_with_shift(dividend, divisor):
    res = 0
    remainder = dividend << 1
    print("Init remainder = {:b}".format(remainder))
    for i in range(0, dividend.bit_length()):
        res = res << 1
        remainder -= divisor << dividend.bit_length()
        spc("=")
        print("remainder = remainder – divisor = {:b}".format(remainder))
        if remainder < 0:
            remainder += divisor << dividend.bit_length()
            remainder = remainder << 1
            print("remainder < 0: remainder -> + divisor, shift 1; res -> shift")
        else:
            remainder = remainder << 1
            remainder += 1
            print("remainder >= 0: remainder -> shift 1, + 1; res -> +1, shift")
            res += 1
        print("remainder = {:b}".format(remainder))
        print("res = {:b}".format(res))
    return res

main_dividend = int(input("Input dividend: "), 2)
main_divisor = int(input("Input divisor: "), 2)
spc("=")
res = devide_with_shift(main_dividend, main_divisor)
spc("=")
print("{:b} (".format(main_dividend), main_dividend,
      ") /  {:b} (".format(main_divisor), main_divisor,
      ") =  {:b} (".format(res), res, ")")
