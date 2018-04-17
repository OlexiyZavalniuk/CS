
def spc(string):
    print(string * 20)

def inverse(value, sign):
    value = value ^ (2**(sign+1) - 1)
    value += 1
    value = value & (2**(sign+1) - 1)
    return value

def Booth(multiplicand, product, sign):
    for i in range(0, sign):
        print("       step", i+1)
        spc("=")
        print("multiplicand: {:b}".format(multiplicand))
        print("product: {:b}".format(product))
        if product & 3 == 1:
            print("01 -> p + m, shift")
            print("  {:11b}".format(product))
            product += multiplicand << (sign + 1)
            product = product & (2**(2 * sign + 1) - 1)
            print("+ {:11b}".format(multiplicand << (sign + 1)))
        elif product & 3 == 2:
            print("10 -> p - m, shift")
            print("  {:11b}".format(product))
            product += inverse(multiplicand, sign) << (sign + 1)
            product = product & (2**(2 * sign + 1) - 1)
            print("+ {:11b}".format(inverse(multiplicand, sign) << (sign + 1)))
        else:
            print("00 or 11 -> shift")
        if product & 2**(2 * sign):
            product += 2**(2 * sign + 1)
        product = product >> 1
        spc("_")
        print("  {:11b}".format(product))
        spc("=")
    product = product >> 1
    return product

main_sign = int(input("Input sign position: "))
main_multiplicand = int(input("Input multiplicand: "), 2)
main_product = int(input("Input multiplier: "), 2)
print("  {:10b}".format(main_multiplicand))
print("* {:10b}".format(main_product))
print("Sign: ", main_sign)
spc("=")
main_product = main_product << 1
main_product = Booth(main_multiplicand, main_product, main_sign)
print("Result:")
print("  {:11b}".format(main_product))
if main_product & (2 ** (2 * main_sign - 1)):
    print("-", inverse(main_product, 2 * main_sign - 1))
else:
    print(main_product)